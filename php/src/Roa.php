<?php

// This file is auto-generated, don't edit it. Thanks.
namespace AlibabaCloud\Tea\Roa;

use AlibabaCloud\Tea\Tea;
use AlibabaCloud\Tea\Model;
use AlibabaCloud\Tea\Request;
use AlibabaCloud\Tea\Exception\TeaError;
use AlibabaCloud\Tea\Exception\TeaUnableRetryError;
use AlibabaCloud\Tea\Utils\Utils;
use AlibabaCloud\Credentials\Credential;
use AlibabaCloud\Tea\RoaUtils\RoaUtils;
use AlibabaCloud\Tea\Utils\Utils\RuntimeOptions;

use AlibabaCloud\Tea\Roa\Roa\Config;

class Roa {
    private $_protocol;
    private $_readTimeout;
    private $_connectTimeout;
    private $_httpProxy;
    private $_httpsProxy;
    private $_noProxy;
    private $_maxIdleConns;
    private $_endpointHost;
    private $_credential;
    public function __construct(Config $config){
        if (Utils::isUnset($config)) {
            throw new TeaError([
                "name" => "ParameterMissing",
                "message" => "'config' can not be unset"
                ]);
        }
        if (Utils::_empty($config->endpoint)) {
            throw new TeaError([
                "name" => "ParameterMissing",
                "message" => "'config.endpoint' can not be empty"
                ]);
        }
        if (Utils::_empty($config->type)) {
            $config->type = "access_key";
        }
        $credentialConfig = new \AlibabaCloud\Credentials\Credential\Config([
            "accessKeyId" => $config->accessKeyId,
            "type" => $config->type,
            "accessKeySecret" => $config->accessKeySecret,
            "securityToken" => $config->securityToken
            ]);
        $this->_credential = new Credential($credentialConfig);
        $this->_protocol = $config->protocol;
        $this->_endpointHost = $config->endpoint;
        $this->_readTimeout = $config->readTimeout;
        $this->_connectTimeout = $config->connectTimeout;
        $this->_httpProxy = $config->httpProxy;
        $this->_httpsProxy = $config->httpsProxy;
        $this->_maxIdleConns = $config->maxIdleConns;
    }

    /**
     * @param string $version
     * @param string $protocol
     * @param string $method
     * @param string $authType
     * @param string $pathname
     * @param array $query
     * @param array $headers
     * @param any $body
     * @param RuntimeOptions $runtime
     * @return object|array
     * @throws \Exception
     */
    public function doRequest($version, $protocol, $method, $authType, $pathname, $query, $headers, $body, RuntimeOptions $runtime){
        $runtime->validate();
        $_runtime = [
            "timeouted" => "retry",
            "readTimeout" => Utils::defaultNumber($runtime->readTimeout, $this->_readTimeout),
            "connectTimeout" => Utils::defaultNumber($runtime->connectTimeout, $this->_connectTimeout),
            "httpProxy" => Utils::defaultString($runtime->httpProxy, $this->_httpProxy),
            "httpsProxy" => Utils::defaultString($runtime->httpsProxy, $this->_httpsProxy),
            "noProxy" => Utils::defaultString($runtime->noProxy, $this->_noProxy),
            "maxIdleConns" => Utils::defaultNumber($runtime->maxIdleConns, $this->_maxIdleConns),
            "retry" => [
                "retryable" => $runtime->autoretry,
                "maxAttempts" => Utils::defaultNumber($runtime->maxAttempts, 3)
                ],
            "backoff" => [
                "policy" => Utils::defaultString($runtime->backoffPolicy, "no"),
                "period" => Utils::defaultNumber($runtime->backoffPeriod, 1)
                ],
            "ignoreSSL" => $runtime->ignoreSSL
            ];
        $_lastRequest = null;
        $_now = time();
        $_retryTimes = 0;
        while (Tea::allowRetry($_runtime["retry"], $_retryTimes, $_now)) {
            if ($_retryTimes > 0) {
                $_backoffTime = Tea::getBackoffTime($_runtime["backoff"], $_retryTimes);
                if ($_backoffTime > 0) {
                    Tea::sleep($_backoffTime);
                }
            }
            $_retryTimes = $_retryTimes + 1;
            try {
                $_request = new Request();
                $_request->protocol = Utils::defaultString($this->_protocol, $protocol);
                $_request->method = $method;
                $_request->pathname = $pathname;
                $_request->headers = Tea::merge([
                    "date" => Utils::getDateUTCString(),
                    "host" => $this->_endpointHost,
                    "accept" => "application/json",
                    "x-acs-signature-nonce" => Utils::getNonce(),
                    "x-acs-signature-method" => "HMAC-SHA1",
                    "x-acs-signature-version" => "1.0",
                    "x-acs-version" => $version,
                    // user-agent': helper.DEFAULT_UA,
                    // x-sdk-client': helper.DEFAULT_CLIENT
                    ], $headers);
                if (!Utils::isUnset($body)) {
                    $_request->body = Utils::toJSONString($body);
                }
                if (!Utils::isUnset($query)) {
                    $_request->query = $query;
                }
                if (!Utils::equalString($authType, "Anonymous")) {
                    $accessKeyId = $this->_credential->getAccessKeyId();
                    $accessKeySecret = $this->_credential->getAccessKeySecret();
                    $securityToken = $this->_credential->getSecurityToken();
                    if (!Utils::_empty($securityToken)) {
                        $_request->headers["x-acs-accesskey-id"] = $accessKeyId;
                        $_request->headers["x-acs-security-token"] = $securityToken;
                    }
                    $stringToSign = RoaUtils::getStringToSign($_request);
                    $_request->headers["authorization"] = "acs " . $accessKeyId . ":" . RoaUtils::getSignature($stringToSign, $accessKeySecret) . "";
                }
                $_lastRequest = $_request;
                $_response= Tea::send($_request, $_runtime);
                if (Utils::equalNumber($_response->statusCode, 204)) {
                    return [
                        "headers" => $_response->headers
                        ];
                }
                $result = Utils::readAsJSON($_response->body);
                if (Utils::is4xx($_response->statusCode) || Utils::is5xx($_response->statusCode)) {
                    $err = Utils::assertAsMap($result);
                    throw new TeaError([
                        "code" => "" . self::defaultAny($err["Code"], $err["code"]) . "Error",
                        "message" => "code: " . $_response->statusCode . ", " . self::defaultAny($err["Message"], $err["message"]) . " requestid: " . self::defaultAny($err["RequestId"], $err["requestId"]) . "",
                        "data" => $err
                        ]);
                }
                return [
                    "headers" => $_response->headers,
                    "body" => $result
                    ];
            }
            catch (\Exception $e) {
                if (Tea::isRetryable($_runtime["retry"], $_retryTimes)) {
                    continue;
                }
                throw $e;
            }
        }
        throw new TeaUnableRetryError($_lastRequest);
    }

    /**
     * @param any $inputValue
     * @param any $defaultValue
     * @return any
     * @throws \Exception
     */
    public static function defaultAny($inputValue, $defaultValue){
        if (Utils::isUnset($inputValue)) {
            return $defaultValue;
        }
        return $inputValue;
    }
}
