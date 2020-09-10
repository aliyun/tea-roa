// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.tearoa;

import com.aliyun.tea.*;
import com.aliyun.tearoa.models.*;

public class Client {

    public String _protocol;
    public Integer _readTimeout;
    public Integer _connectTimeout;
    public String _httpProxy;
    public String _httpsProxy;
    public String _noProxy;
    public Integer _maxIdleConns;
    public String _endpointHost;
    public String _network;
    public String _endpointRule;
    public java.util.Map<String, String> _endpointMap;
    public String _suffix;
    public String _productId;
    public String _regionId;
    public String _userAgent;
    public com.aliyun.credentials.Client _credential;
    /**
     * Init client with Config
     * @param config config contains the necessary information to create a client
     */
    public Client(Config config) {
        try {
            if (com.aliyun.teautil.Common.isUnset(TeaModel.buildMap(config))) {
                throw new TeaException(TeaConverter.buildMap(
                        new TeaPair("code", "ParameterMissing"),
                        new TeaPair("message", "'config' can not be unset")
                ));
            }

            com.aliyun.teautil.Common.validateModel(config);
            if (!com.aliyun.teautil.Common.empty(config.accessKeyId) && !com.aliyun.teautil.Common.empty(config.accessKeySecret)) {
                if (!com.aliyun.teautil.Common.empty(config.securityToken)) {
                    config.type = "sts";
                } else {
                    config.type = "access_key";
                }

                com.aliyun.credentials.models.Config credentialConfig = com.aliyun.credentials.models.Config.build(TeaConverter.buildMap(
                        new TeaPair("accessKeyId", config.accessKeyId),
                        new TeaPair("type", config.type),
                        new TeaPair("accessKeySecret", config.accessKeySecret),
                        new TeaPair("securityToken", config.securityToken)
                ));
                this._credential = new com.aliyun.credentials.Client(credentialConfig);
            } else if (!com.aliyun.teautil.Common.isUnset(config.credential)) {
                this._credential = config.credential;
            } else {
                throw new TeaException(TeaConverter.buildMap(
                        new TeaPair("code", "ParameterMissing"),
                        new TeaPair("message", "'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset")
                ));
            }
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
        this._regionId = config.regionId;
        this._protocol = config.protocol;
        this._endpointHost = config.endpoint;
        this._readTimeout = config.readTimeout;
        this._connectTimeout = config.connectTimeout;
        this._httpProxy = config.httpProxy;
        this._httpsProxy = config.httpsProxy;
        this._maxIdleConns = config.maxIdleConns;
    }

    public java.util.Map<String, ?> doRequest(String version, String protocol, String method, String authType, String pathname, java.util.Map<String, String> query, java.util.Map<String, String> headers, Object body, com.aliyun.teautil.models.RuntimeOptions runtime) {
        java.util.Map<String, Object> runtime_ = TeaConverter.buildMap(
            new TeaPair("timeouted", "retry"),
            new TeaPair("readTimeout", com.aliyun.teautil.Common.defaultNumber(runtime.readTimeout, _readTimeout)),
            new TeaPair("connectTimeout", com.aliyun.teautil.Common.defaultNumber(runtime.connectTimeout, _connectTimeout)),
            new TeaPair("httpProxy", com.aliyun.teautil.Common.defaultString(runtime.httpProxy, _httpProxy)),
            new TeaPair("httpsProxy", com.aliyun.teautil.Common.defaultString(runtime.httpsProxy, _httpsProxy)),
            new TeaPair("noProxy", com.aliyun.teautil.Common.defaultString(runtime.noProxy, _noProxy)),
            new TeaPair("maxIdleConns", com.aliyun.teautil.Common.defaultNumber(runtime.maxIdleConns, _maxIdleConns)),
            new TeaPair("retry", TeaConverter.buildMap(
                new TeaPair("retryable", runtime.autoretry),
                new TeaPair("maxAttempts", com.aliyun.teautil.Common.defaultNumber(runtime.maxAttempts, 3))
            )),
            new TeaPair("backoff", TeaConverter.buildMap(
                new TeaPair("policy", com.aliyun.teautil.Common.defaultString(runtime.backoffPolicy, "no")),
                new TeaPair("period", com.aliyun.teautil.Common.defaultNumber(runtime.backoffPeriod, 1))
            )),
            new TeaPair("ignoreSSL", runtime.ignoreSSL)
        );

        TeaRequest _lastRequest = null;
        long _now = System.currentTimeMillis();
        int _retryTimes = 0;
        while (Tea.allowRetry((java.util.Map<String, Object>) runtime_.get("retry"), _retryTimes, _now)) {
            if (_retryTimes > 0) {
                int backoffTime = Tea.getBackoffTime(runtime_.get("backoff"), _retryTimes);
                if (backoffTime > 0) {
                    try {
                        Tea.sleep(backoffTime);
                    } catch (Exception e) {
                        throw new RuntimeException(e);
                    }
                }
            }
            _retryTimes = _retryTimes + 1;
            try {
                TeaRequest request_ = new TeaRequest();
                request_.protocol = com.aliyun.teautil.Common.defaultString(_protocol, protocol);
                request_.method = method;
                request_.pathname = pathname;
                request_.headers = TeaConverter.merge(String.class,
                    TeaConverter.buildMap(
                        new TeaPair("date", com.aliyun.teautil.Common.getDateUTCString()),
                        new TeaPair("host", _endpointHost),
                        new TeaPair("accept", "application/json"),
                        new TeaPair("x-acs-signature-nonce", com.aliyun.teautil.Common.getNonce()),
                        new TeaPair("x-acs-signature-method", "HMAC-SHA1"),
                        new TeaPair("x-acs-signature-version", "1.0"),
                        new TeaPair("x-acs-version", version),
                        new TeaPair("user-agent", com.aliyun.teautil.Common.getUserAgent(_userAgent))
                    ),
                    headers
                );
                if (!com.aliyun.teautil.Common.isUnset(body)) {
                    request_.body = Tea.toReadable(com.aliyun.teautil.Common.toJSONString(body));
                    request_.headers.put("content-type", "application/json; charset=utf-8");
                }

                if (!com.aliyun.teautil.Common.isUnset(query)) {
                    request_.query = query;
                }

                if (!com.aliyun.teautil.Common.equalString(authType, "Anonymous")) {
                    String accessKeyId = _credential.getAccessKeyId();
                    String accessKeySecret = _credential.getAccessKeySecret();
                    String securityToken = _credential.getSecurityToken();
                    if (!com.aliyun.teautil.Common.empty(securityToken)) {
                        request_.headers.put("x-acs-accesskey-id", accessKeyId);
                        request_.headers.put("x-acs-security-token", securityToken);
                    }

                    String stringToSign = com.aliyun.roautil.Client.getStringToSign(request_);
                    request_.headers.put("authorization", "acs " + accessKeyId + ":" + com.aliyun.roautil.Client.getSignature(stringToSign, accessKeySecret) + "");
                }

                _lastRequest = request_;
                TeaResponse response_ = Tea.doAction(request_, runtime_);

                if (com.aliyun.teautil.Common.equalNumber(response_.statusCode, 204)) {
                    return TeaConverter.buildMap(
                        new TeaPair("headers", response_.headers)
                    );
                }

                Object result = com.aliyun.teautil.Common.readAsJSON(response_.body);
                if (com.aliyun.teautil.Common.is4xx(response_.statusCode) || com.aliyun.teautil.Common.is5xx(response_.statusCode)) {
                    java.util.Map<String, Object> err = com.aliyun.teautil.Common.assertAsMap(result);
                    throw new TeaException(TeaConverter.buildMap(
                        new TeaPair("code", "" + Client.defaultAny(err.get("Code"), err.get("code")) + "Error"),
                        new TeaPair("message", "code: " + response_.statusCode + ", " + Client.defaultAny(err.get("Message"), err.get("message")) + " requestid: " + Client.defaultAny(err.get("RequestId"), err.get("requestId")) + ""),
                        new TeaPair("data", err)
                    ));
                }

                return TeaConverter.buildMap(
                    new TeaPair("headers", response_.headers),
                    new TeaPair("body", result)
                );
            } catch (Exception e) {
                if (Tea.isRetryable(e)) {
                    continue;
                }
                throw new RuntimeException(e);
            }
        }

        throw new RuntimeException(new TeaUnretryableException(_lastRequest));
    }

    public java.util.Map<String, ?> doRequestWithAction(String action, String version, String protocol, String method, String authType, String pathname, java.util.Map<String, String> query, java.util.Map<String, String> headers, Object body, com.aliyun.teautil.models.RuntimeOptions runtime) {
        java.util.Map<String, Object> runtime_ = TeaConverter.buildMap(
            new TeaPair("timeouted", "retry"),
            new TeaPair("readTimeout", com.aliyun.teautil.Common.defaultNumber(runtime.readTimeout, _readTimeout)),
            new TeaPair("connectTimeout", com.aliyun.teautil.Common.defaultNumber(runtime.connectTimeout, _connectTimeout)),
            new TeaPair("httpProxy", com.aliyun.teautil.Common.defaultString(runtime.httpProxy, _httpProxy)),
            new TeaPair("httpsProxy", com.aliyun.teautil.Common.defaultString(runtime.httpsProxy, _httpsProxy)),
            new TeaPair("noProxy", com.aliyun.teautil.Common.defaultString(runtime.noProxy, _noProxy)),
            new TeaPair("maxIdleConns", com.aliyun.teautil.Common.defaultNumber(runtime.maxIdleConns, _maxIdleConns)),
            new TeaPair("retry", TeaConverter.buildMap(
                new TeaPair("retryable", runtime.autoretry),
                new TeaPair("maxAttempts", com.aliyun.teautil.Common.defaultNumber(runtime.maxAttempts, 3))
            )),
            new TeaPair("backoff", TeaConverter.buildMap(
                new TeaPair("policy", com.aliyun.teautil.Common.defaultString(runtime.backoffPolicy, "no")),
                new TeaPair("period", com.aliyun.teautil.Common.defaultNumber(runtime.backoffPeriod, 1))
            )),
            new TeaPair("ignoreSSL", runtime.ignoreSSL)
        );

        TeaRequest _lastRequest = null;
        long _now = System.currentTimeMillis();
        int _retryTimes = 0;
        while (Tea.allowRetry((java.util.Map<String, Object>) runtime_.get("retry"), _retryTimes, _now)) {
            if (_retryTimes > 0) {
                int backoffTime = Tea.getBackoffTime(runtime_.get("backoff"), _retryTimes);
                if (backoffTime > 0) {
                    try {
                        Tea.sleep(backoffTime);
                    } catch (InterruptedException e) {
                        throw new RuntimeException(e);
                    }
                }
            }
            _retryTimes = _retryTimes + 1;
            try {
                TeaRequest request_ = new TeaRequest();
                request_.protocol = com.aliyun.teautil.Common.defaultString(_protocol, protocol);
                request_.method = method;
                request_.pathname = pathname;
                request_.headers = TeaConverter.merge(String.class,
                    TeaConverter.buildMap(
                        new TeaPair("date", com.aliyun.teautil.Common.getDateUTCString()),
                        new TeaPair("host", _endpointHost),
                        new TeaPair("accept", "application/json"),
                        new TeaPair("x-acs-signature-nonce", com.aliyun.teautil.Common.getNonce()),
                        new TeaPair("x-acs-signature-method", "HMAC-SHA1"),
                        new TeaPair("x-acs-signature-version", "1.0"),
                        new TeaPair("x-acs-version", version),
                        new TeaPair("x-acs-action", action),
                        new TeaPair("user-agent", com.aliyun.teautil.Common.getUserAgent(_userAgent))
                    ),
                    headers
                );
                if (!com.aliyun.teautil.Common.isUnset(body)) {
                    request_.body = Tea.toReadable(com.aliyun.teautil.Common.toJSONString(body));
                    request_.headers.put("content-type", "application/json; charset=utf-8");
                }

                if (!com.aliyun.teautil.Common.isUnset(query)) {
                    request_.query = query;
                }

                if (!com.aliyun.teautil.Common.equalString(authType, "Anonymous")) {
                    String accessKeyId = _credential.getAccessKeyId();
                    String accessKeySecret = _credential.getAccessKeySecret();
                    String securityToken = _credential.getSecurityToken();
                    if (!com.aliyun.teautil.Common.empty(securityToken)) {
                        request_.headers.put("x-acs-accesskey-id", accessKeyId);
                        request_.headers.put("x-acs-security-token", securityToken);
                    }

                    String stringToSign = com.aliyun.roautil.Client.getStringToSign(request_);
                    request_.headers.put("authorization", "acs " + accessKeyId + ":" + com.aliyun.roautil.Client.getSignature(stringToSign, accessKeySecret) + "");
                }

                _lastRequest = request_;
                TeaResponse response_ = Tea.doAction(request_, runtime_);

                if (com.aliyun.teautil.Common.equalNumber(response_.statusCode, 204)) {
                    return TeaConverter.buildMap(
                        new TeaPair("headers", response_.headers)
                    );
                }

                Object result = com.aliyun.teautil.Common.readAsJSON(response_.body);
                if (com.aliyun.teautil.Common.is4xx(response_.statusCode) || com.aliyun.teautil.Common.is5xx(response_.statusCode)) {
                    java.util.Map<String, Object> err = com.aliyun.teautil.Common.assertAsMap(result);
                    throw new TeaException(TeaConverter.buildMap(
                        new TeaPair("code", "" + Client.defaultAny(err.get("Code"), err.get("code")) + "Error"),
                        new TeaPair("message", "code: " + response_.statusCode + ", " + Client.defaultAny(err.get("Message"), err.get("message")) + " requestid: " + Client.defaultAny(err.get("RequestId"), err.get("requestId")) + ""),
                        new TeaPair("data", err)
                    ));
                }

                return TeaConverter.buildMap(
                    new TeaPair("headers", response_.headers),
                    new TeaPair("body", result)
                );
            } catch (Exception e) {
                if (Tea.isRetryable(e)) {
                    continue;
                }
                throw new RuntimeException(e);
            }
        }

        throw new RuntimeException(new TeaUnretryableException(_lastRequest));
    }

    public java.util.Map<String, ?> doRequestWithForm(String version, String protocol, String method, String authType, String pathname, java.util.Map<String, String> query, java.util.Map<String, String> headers, java.util.Map<String, ?> body, com.aliyun.teautil.models.RuntimeOptions runtime) {
        java.util.Map<String, Object> runtime_ = TeaConverter.buildMap(
            new TeaPair("timeouted", "retry"),
            new TeaPair("readTimeout", com.aliyun.teautil.Common.defaultNumber(runtime.readTimeout, _readTimeout)),
            new TeaPair("connectTimeout", com.aliyun.teautil.Common.defaultNumber(runtime.connectTimeout, _connectTimeout)),
            new TeaPair("httpProxy", com.aliyun.teautil.Common.defaultString(runtime.httpProxy, _httpProxy)),
            new TeaPair("httpsProxy", com.aliyun.teautil.Common.defaultString(runtime.httpsProxy, _httpsProxy)),
            new TeaPair("noProxy", com.aliyun.teautil.Common.defaultString(runtime.noProxy, _noProxy)),
            new TeaPair("maxIdleConns", com.aliyun.teautil.Common.defaultNumber(runtime.maxIdleConns, _maxIdleConns)),
            new TeaPair("retry", TeaConverter.buildMap(
                new TeaPair("retryable", runtime.autoretry),
                new TeaPair("maxAttempts", com.aliyun.teautil.Common.defaultNumber(runtime.maxAttempts, 3))
            )),
            new TeaPair("backoff", TeaConverter.buildMap(
                new TeaPair("policy", com.aliyun.teautil.Common.defaultString(runtime.backoffPolicy, "no")),
                new TeaPair("period", com.aliyun.teautil.Common.defaultNumber(runtime.backoffPeriod, 1))
            )),
            new TeaPair("ignoreSSL", runtime.ignoreSSL)
        );

        TeaRequest _lastRequest = null;
        long _now = System.currentTimeMillis();
        int _retryTimes = 0;
        while (Tea.allowRetry((java.util.Map<String, Object>) runtime_.get("retry"), _retryTimes, _now)) {
            if (_retryTimes > 0) {
                int backoffTime = Tea.getBackoffTime(runtime_.get("backoff"), _retryTimes);
                if (backoffTime > 0) {
                    try {
                        Tea.sleep(backoffTime);
                    } catch (InterruptedException e) {
                        throw new RuntimeException(e);
                    }
                }
            }
            _retryTimes = _retryTimes + 1;
            try {
                TeaRequest request_ = new TeaRequest();
                request_.protocol = com.aliyun.teautil.Common.defaultString(_protocol, protocol);
                request_.method = method;
                request_.pathname = pathname;
                request_.headers = TeaConverter.merge(String.class,
                    TeaConverter.buildMap(
                        new TeaPair("date", com.aliyun.teautil.Common.getDateUTCString()),
                        new TeaPair("host", _endpointHost),
                        new TeaPair("accept", "application/json"),
                        new TeaPair("x-acs-signature-nonce", com.aliyun.teautil.Common.getNonce()),
                        new TeaPair("x-acs-signature-method", "HMAC-SHA1"),
                        new TeaPair("x-acs-signature-version", "1.0"),
                        new TeaPair("x-acs-version", version),
                        new TeaPair("user-agent", com.aliyun.teautil.Common.getUserAgent(_userAgent))
                    ),
                    headers
                );
                if (!com.aliyun.teautil.Common.isUnset(body)) {
                    request_.body = Tea.toReadable(com.aliyun.roautil.Client.toForm(body));
                    request_.headers.put("content-type", "application/x-www-form-urlencoded");
                }

                if (!com.aliyun.teautil.Common.isUnset(query)) {
                    request_.query = query;
                }

                if (!com.aliyun.teautil.Common.equalString(authType, "Anonymous")) {
                    String accessKeyId = _credential.getAccessKeyId();
                    String accessKeySecret = _credential.getAccessKeySecret();
                    String securityToken = _credential.getSecurityToken();
                    if (!com.aliyun.teautil.Common.empty(securityToken)) {
                        request_.headers.put("x-acs-accesskey-id", accessKeyId);
                        request_.headers.put("x-acs-security-token", securityToken);
                    }

                    String stringToSign = com.aliyun.roautil.Client.getStringToSign(request_);
                    request_.headers.put("authorization", "acs " + accessKeyId + ":" + com.aliyun.roautil.Client.getSignature(stringToSign, accessKeySecret) + "");
                }

                _lastRequest = request_;
                TeaResponse response_ = Tea.doAction(request_, runtime_);

                if (com.aliyun.teautil.Common.equalNumber(response_.statusCode, 204)) {
                    return TeaConverter.buildMap(
                        new TeaPair("headers", response_.headers)
                    );
                }

                Object result = com.aliyun.teautil.Common.readAsJSON(response_.body);
                if (com.aliyun.teautil.Common.is4xx(response_.statusCode) || com.aliyun.teautil.Common.is5xx(response_.statusCode)) {
                    java.util.Map<String, Object> err = com.aliyun.teautil.Common.assertAsMap(result);
                    throw new TeaException(TeaConverter.buildMap(
                        new TeaPair("code", "" + Client.defaultAny(err.get("Code"), err.get("code")) + "Error"),
                        new TeaPair("message", "code: " + response_.statusCode + ", " + Client.defaultAny(err.get("Message"), err.get("message")) + " requestid: " + Client.defaultAny(err.get("RequestId"), err.get("requestId")) + ""),
                        new TeaPair("data", err)
                    ));
                }

                return TeaConverter.buildMap(
                    new TeaPair("headers", response_.headers),
                    new TeaPair("body", result)
                );
            } catch (Exception e) {
                if (Tea.isRetryable(e)) {
                    continue;
                }
                throw new RuntimeException(e);
            }
        }

        throw new RuntimeException(new TeaUnretryableException(_lastRequest));
    }

    /**
     * If inputValue is not null, return it or return defaultValue
     * @param inputValue  users input value
     * @param defaultValue default value
     * @return the final result
     */
    public static Object defaultAny(Object inputValue, Object defaultValue) {
        if (com.aliyun.teautil.Common.isUnset(inputValue)) {
            return defaultValue;
        }

        return inputValue;
    }

    /**
     * If the endpointRule and config.endpoint are empty, throw error
     * @param config config contains the necessary information to create a client
     */
    public void checkConfig(Config config) {
        if (com.aliyun.teautil.Common.empty(_endpointRule) && com.aliyun.teautil.Common.empty(config.endpoint)) {
            throw new RuntimeException("'config.endpoint' can not be empty");
        }
    }
}
