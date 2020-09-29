<?php

// This file is auto-generated, don't edit it. Thanks.

namespace AlibabaCloud\Tea\Roa;

use AlibabaCloud\Credentials\Credential;
use AlibabaCloud\Tea\Exception\TeaError;
use AlibabaCloud\Tea\Exception\TeaUnableRetryError;
use AlibabaCloud\Tea\Request;
use AlibabaCloud\Tea\Roa\Roa\Config;
use AlibabaCloud\Tea\RoaUtils\RoaUtils;
use AlibabaCloud\Tea\Tea;
use AlibabaCloud\Tea\Utils\Utils;
use AlibabaCloud\Tea\Utils\Utils\RuntimeOptions;
use Exception;

/**
 * This is for ROA SDK.
 */
class Roa
{
    protected $_protocol;

    protected $_readTimeout;

    protected $_connectTimeout;

    protected $_httpProxy;

    protected $_httpsProxy;

    protected $_noProxy;

    protected $_maxIdleConns;

    protected $_endpointHost;

    protected $_network;

    protected $_endpointRule;

    protected $_endpointMap;

    protected $_suffix;

    protected $_productId;

    protected $_regionId;

    protected $_userAgent;

    protected $_credential;

    /**
     * Init client with Config.
     *
     * @param config config contains the necessary information to create a client
     * @param mixed $config
     */
    public function __construct($config)
    {
        if (Utils::isUnset($config)) {
            throw new TeaError([
                'code'    => 'ParameterMissing',
                'message' => "'config' can not be unset",
            ]);
        }
        Utils::validateModel($config);
        if (!Utils::empty_($config->accessKeyId) && !Utils::empty_($config->accessKeySecret)) {
            if (!Utils::empty_($config->securityToken)) {
                $config->type = 'sts';
            } else {
                $config->type = 'access_key';
            }
            $credentialConfig = new \AlibabaCloud\Credentials\Credential\Config([
                'accessKeyId'     => $config->accessKeyId,
                'type'            => $config->type,
                'accessKeySecret' => $config->accessKeySecret,
                'securityToken'   => $config->securityToken,
            ]);
            $this->_credential = new Credential($credentialConfig);
        } elseif (!Utils::isUnset($config->credential)) {
            $this->_credential = $config->credential;
        } else {
            throw new TeaError([
                'code'    => 'ParameterMissing',
                'message' => "'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset",
            ]);
        }
        $this->_regionId       = $config->regionId;
        $this->_protocol       = $config->protocol;
        $this->_endpointHost   = $config->endpoint;
        $this->_readTimeout    = $config->readTimeout;
        $this->_connectTimeout = $config->connectTimeout;
        $this->_httpProxy      = $config->httpProxy;
        $this->_httpsProxy     = $config->httpsProxy;
        $this->_maxIdleConns   = $config->maxIdleConns;
    }

    /**
     * Encapsulate the request and invoke the network.
     *
     * @param string         $version  product version
     * @param string         $protocol http or https
     * @param string         $method   e.g. GET
     * @param string         $authType when authType is Anonymous, the signature will not be calculate
     * @param string         $pathname pathname of every api
     * @param string[]       $query    which contains request params
     * @param string[]       $headers  request headers
     * @param mixed          $body     content of request
     * @param RuntimeOptions $runtime  which controls some details of call api, such as retry times
     *
     * @throws TeaError
     * @throws Exception
     * @throws TeaUnableRetryError
     *
     * @return array the response
     */
    public function doRequest($version, $protocol, $method, $authType, $pathname, $query, $headers, $body, $runtime)
    {
        $runtime->validate();
        $_runtime = [
            'timeouted'      => 'retry',
            'readTimeout'    => Utils::defaultNumber($runtime->readTimeout, $this->_readTimeout),
            'connectTimeout' => Utils::defaultNumber($runtime->connectTimeout, $this->_connectTimeout),
            'httpProxy'      => Utils::defaultString($runtime->httpProxy, $this->_httpProxy),
            'httpsProxy'     => Utils::defaultString($runtime->httpsProxy, $this->_httpsProxy),
            'noProxy'        => Utils::defaultString($runtime->noProxy, $this->_noProxy),
            'maxIdleConns'   => Utils::defaultNumber($runtime->maxIdleConns, $this->_maxIdleConns),
            'retry'          => [
                'retryable'   => $runtime->autoretry,
                'maxAttempts' => Utils::defaultNumber($runtime->maxAttempts, 3),
            ],
            'backoff' => [
                'policy' => Utils::defaultString($runtime->backoffPolicy, 'no'),
                'period' => Utils::defaultNumber($runtime->backoffPeriod, 1),
            ],
            'ignoreSSL' => $runtime->ignoreSSL,
        ];
        $_lastRequest   = null;
        $_lastException = null;
        $_now           = time();
        $_retryTimes    = 0;
        while (Tea::allowRetry(@$_runtime['retry'], $_retryTimes, $_now)) {
            if ($_retryTimes > 0) {
                $_backoffTime = Tea::getBackoffTime(@$_runtime['backoff'], $_retryTimes);
                if ($_backoffTime > 0) {
                    Tea::sleep($_backoffTime);
                }
            }
            $_retryTimes = $_retryTimes + 1;

            try {
                $_request           = new Request();
                $_request->protocol = Utils::defaultString($this->_protocol, $protocol);
                $_request->method   = $method;
                $_request->pathname = $pathname;
                $_request->headers  = Tea::merge([
                    'date'                    => Utils::getDateUTCString(),
                    'host'                    => $this->_endpointHost,
                    'accept'                  => 'application/json',
                    'x-acs-signature-nonce'   => Utils::getNonce(),
                    'x-acs-signature-method'  => 'HMAC-SHA1',
                    'x-acs-signature-version' => '1.0',
                    'x-acs-version'           => $version,
                    'user-agent'              => Utils::getUserAgent($this->_userAgent),
                    // x-sdk-client': helper.DEFAULT_CLIENT
                ], $headers);
                if (!Utils::isUnset($body)) {
                    $_request->body                    = Utils::toJSONString($body);
                    $_request->headers['content-type'] = 'application/json; charset=utf-8';
                }
                if (!Utils::isUnset($query)) {
                    $_request->query = $query;
                }
                if (!Utils::equalString($authType, 'Anonymous')) {
                    $accessKeyId     = $this->_credential->getAccessKeyId();
                    $accessKeySecret = $this->_credential->getAccessKeySecret();
                    $securityToken   = $this->_credential->getSecurityToken();
                    if (!Utils::empty_($securityToken)) {
                        $_request->headers['x-acs-accesskey-id']   = $accessKeyId;
                        $_request->headers['x-acs-security-token'] = $securityToken;
                    }
                    $stringToSign                       = RoaUtils::getStringToSign($_request);
                    $_request->headers['authorization'] = 'acs ' . $accessKeyId . ':' . RoaUtils::getSignature($stringToSign, $accessKeySecret) . '';
                }
                $_lastRequest = $_request;
                $_response    = Tea::send($_request, $_runtime);
                if (Utils::equalNumber($_response->statusCode, 204)) {
                    return [
                        'headers' => $_response->headers,
                    ];
                }
                $result = Utils::readAsJSON($_response->body);
                if (Utils::is4xx($_response->statusCode) || Utils::is5xx($_response->statusCode)) {
                    $err = Utils::assertAsMap($result);

                    throw new TeaError([
                        'code'    => '' . (string) (self::defaultAny(@$err['Code'], @$err['code'])) . '',
                        'message' => 'code: ' . (string) ($_response->statusCode) . ', ' . (string) (self::defaultAny(@$err['Message'], @$err['message'])) . ' request id: ' . (string) (self::defaultAny(@$err['RequestId'], @$err['requestId'])) . '',
                        'data'    => $err,
                    ]);
                }

                return [
                    'headers' => $_response->headers,
                    'body'    => $result,
                ];
            } catch (Exception $e) {
                if (!($e instanceof TeaError)) {
                    $e = new TeaError([], $e->getMessage(), $e->getCode(), $e);
                }
                if (Tea::isRetryable($e)) {
                    $_lastException = $e;

                    continue;
                }

                throw $e;
            }
        }

        throw new TeaUnableRetryError($_lastRequest, $_lastException);
    }

    /**
     * Encapsulate the request and invoke the network.
     *
     * @param string         $action   api name
     * @param string         $version  product version
     * @param string         $protocol http or https
     * @param string         $method   e.g. GET
     * @param string         $authType when authType is Anonymous, the signature will not be calculate
     * @param string         $pathname pathname of every api
     * @param string[]       $query    which contains request params
     * @param string[]       $headers  request headers
     * @param mixed          $body     content of request
     * @param RuntimeOptions $runtime  which controls some details of call api, such as retry times
     *
     * @throws TeaError
     * @throws Exception
     * @throws TeaUnableRetryError
     *
     * @return array the response
     */
    public function doRequestWithAction($action, $version, $protocol, $method, $authType, $pathname, $query, $headers, $body, $runtime)
    {
        $runtime->validate();
        $_runtime = [
            'timeouted'      => 'retry',
            'readTimeout'    => Utils::defaultNumber($runtime->readTimeout, $this->_readTimeout),
            'connectTimeout' => Utils::defaultNumber($runtime->connectTimeout, $this->_connectTimeout),
            'httpProxy'      => Utils::defaultString($runtime->httpProxy, $this->_httpProxy),
            'httpsProxy'     => Utils::defaultString($runtime->httpsProxy, $this->_httpsProxy),
            'noProxy'        => Utils::defaultString($runtime->noProxy, $this->_noProxy),
            'maxIdleConns'   => Utils::defaultNumber($runtime->maxIdleConns, $this->_maxIdleConns),
            'retry'          => [
                'retryable'   => $runtime->autoretry,
                'maxAttempts' => Utils::defaultNumber($runtime->maxAttempts, 3),
            ],
            'backoff' => [
                'policy' => Utils::defaultString($runtime->backoffPolicy, 'no'),
                'period' => Utils::defaultNumber($runtime->backoffPeriod, 1),
            ],
            'ignoreSSL' => $runtime->ignoreSSL,
        ];
        $_lastRequest   = null;
        $_lastException = null;
        $_now           = time();
        $_retryTimes    = 0;
        while (Tea::allowRetry(@$_runtime['retry'], $_retryTimes, $_now)) {
            if ($_retryTimes > 0) {
                $_backoffTime = Tea::getBackoffTime(@$_runtime['backoff'], $_retryTimes);
                if ($_backoffTime > 0) {
                    Tea::sleep($_backoffTime);
                }
            }
            $_retryTimes = $_retryTimes + 1;

            try {
                $_request           = new Request();
                $_request->protocol = Utils::defaultString($this->_protocol, $protocol);
                $_request->method   = $method;
                $_request->pathname = $pathname;
                $_request->headers  = Tea::merge([
                    'date'                    => Utils::getDateUTCString(),
                    'host'                    => $this->_endpointHost,
                    'accept'                  => 'application/json',
                    'x-acs-signature-nonce'   => Utils::getNonce(),
                    'x-acs-signature-method'  => 'HMAC-SHA1',
                    'x-acs-signature-version' => '1.0',
                    'x-acs-version'           => $version,
                    'x-acs-action'            => $action,
                    'user-agent'              => Utils::getUserAgent($this->_userAgent),
                    // x-sdk-client': helper.DEFAULT_CLIENT
                ], $headers);
                if (!Utils::isUnset($body)) {
                    $_request->body                    = Utils::toJSONString($body);
                    $_request->headers['content-type'] = 'application/json; charset=utf-8';
                }
                if (!Utils::isUnset($query)) {
                    $_request->query = $query;
                }
                if (!Utils::equalString($authType, 'Anonymous')) {
                    $accessKeyId     = $this->_credential->getAccessKeyId();
                    $accessKeySecret = $this->_credential->getAccessKeySecret();
                    $securityToken   = $this->_credential->getSecurityToken();
                    if (!Utils::empty_($securityToken)) {
                        $_request->headers['x-acs-accesskey-id']   = $accessKeyId;
                        $_request->headers['x-acs-security-token'] = $securityToken;
                    }
                    $stringToSign                       = RoaUtils::getStringToSign($_request);
                    $_request->headers['authorization'] = 'acs ' . $accessKeyId . ':' . RoaUtils::getSignature($stringToSign, $accessKeySecret) . '';
                }
                $_lastRequest = $_request;
                $_response    = Tea::send($_request, $_runtime);
                if (Utils::equalNumber($_response->statusCode, 204)) {
                    return [
                        'headers' => $_response->headers,
                    ];
                }
                $result = Utils::readAsJSON($_response->body);
                if (Utils::is4xx($_response->statusCode) || Utils::is5xx($_response->statusCode)) {
                    $err = Utils::assertAsMap($result);

                    throw new TeaError([
                        'code'    => '' . (string) (self::defaultAny(@$err['Code'], @$err['code'])) . '',
                        'message' => 'code: ' . (string) ($_response->statusCode) . ', ' . (string) (self::defaultAny(@$err['Message'], @$err['message'])) . ' request id: ' . (string) (self::defaultAny(@$err['RequestId'], @$err['requestId'])) . '',
                        'data'    => $err,
                    ]);
                }

                return [
                    'headers' => $_response->headers,
                    'body'    => $result,
                ];
            } catch (Exception $e) {
                if (!($e instanceof TeaError)) {
                    $e = new TeaError([], $e->getMessage(), $e->getCode(), $e);
                }
                if (Tea::isRetryable($e)) {
                    $_lastException = $e;

                    continue;
                }

                throw $e;
            }
        }

        throw new TeaUnableRetryError($_lastRequest, $_lastException);
    }

    /**
     * Encapsulate the request and invoke the network.
     *
     * @param string         $version  product version
     * @param string         $protocol http or https
     * @param string         $method   e.g. GET
     * @param string         $authType when authType is Anonymous, the signature will not be calculate
     * @param string         $pathname pathname of every api
     * @param string[]       $query    which contains request params
     * @param string[]       $headers  request headers
     * @param mixed[]        $body     content of request
     * @param RuntimeOptions $runtime  which controls some details of call api, such as retry times
     *
     * @throws TeaError
     * @throws Exception
     * @throws TeaUnableRetryError
     *
     * @return array the response
     */
    public function doRequestWithForm($version, $protocol, $method, $authType, $pathname, $query, $headers, $body, $runtime)
    {
        $runtime->validate();
        $_runtime = [
            'timeouted'      => 'retry',
            'readTimeout'    => Utils::defaultNumber($runtime->readTimeout, $this->_readTimeout),
            'connectTimeout' => Utils::defaultNumber($runtime->connectTimeout, $this->_connectTimeout),
            'httpProxy'      => Utils::defaultString($runtime->httpProxy, $this->_httpProxy),
            'httpsProxy'     => Utils::defaultString($runtime->httpsProxy, $this->_httpsProxy),
            'noProxy'        => Utils::defaultString($runtime->noProxy, $this->_noProxy),
            'maxIdleConns'   => Utils::defaultNumber($runtime->maxIdleConns, $this->_maxIdleConns),
            'retry'          => [
                'retryable'   => $runtime->autoretry,
                'maxAttempts' => Utils::defaultNumber($runtime->maxAttempts, 3),
            ],
            'backoff' => [
                'policy' => Utils::defaultString($runtime->backoffPolicy, 'no'),
                'period' => Utils::defaultNumber($runtime->backoffPeriod, 1),
            ],
            'ignoreSSL' => $runtime->ignoreSSL,
        ];
        $_lastRequest   = null;
        $_lastException = null;
        $_now           = time();
        $_retryTimes    = 0;
        while (Tea::allowRetry(@$_runtime['retry'], $_retryTimes, $_now)) {
            if ($_retryTimes > 0) {
                $_backoffTime = Tea::getBackoffTime(@$_runtime['backoff'], $_retryTimes);
                if ($_backoffTime > 0) {
                    Tea::sleep($_backoffTime);
                }
            }
            $_retryTimes = $_retryTimes + 1;

            try {
                $_request           = new Request();
                $_request->protocol = Utils::defaultString($this->_protocol, $protocol);
                $_request->method   = $method;
                $_request->pathname = $pathname;
                $_request->headers  = Tea::merge([
                    'date'                    => Utils::getDateUTCString(),
                    'host'                    => $this->_endpointHost,
                    'accept'                  => 'application/json',
                    'x-acs-signature-nonce'   => Utils::getNonce(),
                    'x-acs-signature-method'  => 'HMAC-SHA1',
                    'x-acs-signature-version' => '1.0',
                    'x-acs-version'           => $version,
                    'user-agent'              => Utils::getUserAgent($this->_userAgent),
                    // x-sdk-client': helper.DEFAULT_CLIENT
                ], $headers);
                if (!Utils::isUnset($body)) {
                    $_request->body                    = RoaUtils::toForm($body);
                    $_request->headers['content-type'] = 'application/x-www-form-urlencoded';
                }
                if (!Utils::isUnset($query)) {
                    $_request->query = $query;
                }
                if (!Utils::equalString($authType, 'Anonymous')) {
                    $accessKeyId     = $this->_credential->getAccessKeyId();
                    $accessKeySecret = $this->_credential->getAccessKeySecret();
                    $securityToken   = $this->_credential->getSecurityToken();
                    if (!Utils::empty_($securityToken)) {
                        $_request->headers['x-acs-accesskey-id']   = $accessKeyId;
                        $_request->headers['x-acs-security-token'] = $securityToken;
                    }
                    $stringToSign                       = RoaUtils::getStringToSign($_request);
                    $_request->headers['authorization'] = 'acs ' . $accessKeyId . ':' . RoaUtils::getSignature($stringToSign, $accessKeySecret) . '';
                }
                $_lastRequest = $_request;
                $_response    = Tea::send($_request, $_runtime);
                if (Utils::equalNumber($_response->statusCode, 204)) {
                    return [
                        'headers' => $_response->headers,
                    ];
                }
                $result = Utils::readAsJSON($_response->body);
                if (Utils::is4xx($_response->statusCode) || Utils::is5xx($_response->statusCode)) {
                    $err = Utils::assertAsMap($result);

                    throw new TeaError([
                        'code'    => '' . (string) (self::defaultAny(@$err['Code'], @$err['code'])) . '',
                        'message' => 'code: ' . (string) ($_response->statusCode) . ', ' . (string) (self::defaultAny(@$err['Message'], @$err['message'])) . ' request id: ' . (string) (self::defaultAny(@$err['RequestId'], @$err['requestId'])) . '',
                        'data'    => $err,
                    ]);
                }

                return [
                    'headers' => $_response->headers,
                    'body'    => $result,
                ];
            } catch (Exception $e) {
                if (!($e instanceof TeaError)) {
                    $e = new TeaError([], $e->getMessage(), $e->getCode(), $e);
                }
                if (Tea::isRetryable($e)) {
                    $_lastException = $e;

                    continue;
                }

                throw $e;
            }
        }

        throw new TeaUnableRetryError($_lastRequest, $_lastException);
    }

    /**
     * If inputValue is not null, return it or return defaultValue.
     *
     * @param mixed $inputValue   users input value
     * @param mixed $defaultValue default value
     *
     * @return any the final result
     */
    public static function defaultAny($inputValue, $defaultValue)
    {
        if (Utils::isUnset($inputValue)) {
            return $defaultValue;
        }

        return $inputValue;
    }

    /**
     * If the endpointRule and config.endpoint are empty, throw error.
     *
     * @param Config $config config contains the necessary information to create a client
     *
     * @throws TeaError
     */
    public function checkConfig($config)
    {
        if (Utils::empty_($this->_endpointRule) && Utils::empty_($config->endpoint)) {
            throw new TeaError([
                'code'    => 'ParameterMissing',
                'message' => "'config.endpoint' can not be empty",
            ]);
        }
    }
}
