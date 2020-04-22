// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

using AlibabaCloud.ROAClient.Models;

namespace AlibabaCloud.ROAClient
{
    public class Client 
    {
        protected string _protocol;
        protected int? _readTimeout;
        protected int? _connectTimeout;
        protected string _httpProxy;
        protected string _httpsProxy;
        protected string _noProxy;
        protected int? _maxIdleConns;
        protected string _endpointHost;
        protected string _network;
        protected string _endpointRule;
        protected Dictionary<string, string> _endpointMap;
        protected string _suffix;
        protected string _productId;
        protected string _regionId;
        protected Aliyun.Credentials.Client _credential;

        public Client(Config config)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(config.ToMap()))
            {
                throw new TeaException(new Dictionary<string, string>
                {
                    {"code", "ParameterMissing"},
                    {"message", "'config' can not be unset"},
                });
            }
            if (!AlibabaCloud.TeaUtil.Common.Empty(config.AccessKeyId) && !AlibabaCloud.TeaUtil.Common.Empty(config.AccessKeySecret))
            {
                if (!AlibabaCloud.TeaUtil.Common.Empty(config.SecurityToken))
                {
                    config.Type = "sts";
                }
                else
                {
                    config.Type = "access_key";
                }
                Aliyun.Credentials.Models.Config credentialConfig = new Aliyun.Credentials.Models.Config
                {
                    AccessKeyId = config.AccessKeyId,
                    Type = config.Type,
                    AccessKeySecret = config.AccessKeySecret,
                    SecurityToken = config.SecurityToken,
                };
                this._credential = new Aliyun.Credentials.Client(credentialConfig);
            }
            else if (!AlibabaCloud.TeaUtil.Common.IsUnset(config.Credential))
            {
                this._credential = config.Credential;
            }
            else
            {
                throw new TeaException(new Dictionary<string, string>
                {
                    {"code", "ParameterMissing"},
                    {"message", "'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset"},
                });
            }
            this._network = config.Network;
            this._regionId = config.RegionId;
            this._suffix = config.Suffix;
            this._protocol = config.Protocol;
            this._endpointHost = config.Endpoint;
            this._readTimeout = config.ReadTimeout;
            this._connectTimeout = config.ConnectTimeout;
            this._httpProxy = config.HttpProxy;
            this._httpsProxy = config.HttpsProxy;
            this._maxIdleConns = config.MaxIdleConns;
        }

        public Dictionary<string, object> DoRequest(string version, string protocol, string method, string authType, string pathname, Dictionary<string, string> query, Dictionary<string, string> headers, object body, AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.ReadTimeout, _readTimeout)},
                {"connectTimeout", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.ConnectTimeout, _connectTimeout)},
                {"httpProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.HttpProxy, _httpProxy)},
                {"httpsProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.HttpsProxy, _httpsProxy)},
                {"noProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.NoProxy, _noProxy)},
                {"maxIdleConns", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxIdleConns, _maxIdleConns)},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, protocol);
                    request_.Method = method;
                    request_.Pathname = pathname;
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"date", AlibabaCloud.TeaUtil.Common.GetDateUTCString()},
                            {"host", _endpointHost},
                            {"accept", "application/json"},
                            {"x-acs-signature-nonce", AlibabaCloud.TeaUtil.Common.GetNonce()},
                            {"x-acs-signature-method", "HMAC-SHA1"},
                            {"x-acs-signature-version", "1.0"},
                            {"x-acs-version", version},
                        },
                        headers
                    );
                    if (!AlibabaCloud.TeaUtil.Common.IsUnset(body))
                    {
                        request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(body));
                    }
                    if (!AlibabaCloud.TeaUtil.Common.IsUnset(query))
                    {
                        request_.Query = query;
                    }
                    if (!AlibabaCloud.TeaUtil.Common.EqualString(authType, "Anonymous"))
                    {
                        string accessKeyId = this._credential.GetAccessKeyId();
                        string accessKeySecret = this._credential.GetAccessKeySecret();
                        string securityToken = this._credential.GetSecurityToken();
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-accesskey-id"] = accessKeyId;
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accessKeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    _lastRequest = request_;
                    TeaResponse response_ = TeaCore.DoAction(request_, runtime_);

                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return new Dictionary<string, object>
                        {
                            {"headers", response_.Headers},
                        };
                    }
                    object result = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    if (AlibabaCloud.TeaUtil.Common.Is4xx(response_.StatusCode) || AlibabaCloud.TeaUtil.Common.Is5xx(response_.StatusCode))
                    {
                        Dictionary<string, object> err = AlibabaCloud.TeaUtil.Common.AssertAsMap(result);
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"code", Client.DefaultAny(err.Get("Code"), err.Get("code")) + "Error"},
                            {"message", "code: " + response_.StatusCode + ", " + Client.DefaultAny(err.Get("Message"), err.Get("message")) + " requestid: " + Client.DefaultAny(err.Get("RequestId"), err.Get("requestId"))},
                            {"data", err},
                        });
                    }
                    return new Dictionary<string, object>
                    {
                        {"headers", response_.Headers},
                        {"body", result},
                    };
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        public async Task<Dictionary<string, object>> DoRequestAsync(string version, string protocol, string method, string authType, string pathname, Dictionary<string, string> query, Dictionary<string, string> headers, object body, AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime)
        {
            Dictionary<string, object> runtime_ = new Dictionary<string, object>
            {
                {"timeouted", "retry"},
                {"readTimeout", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.ReadTimeout, _readTimeout)},
                {"connectTimeout", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.ConnectTimeout, _connectTimeout)},
                {"httpProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.HttpProxy, _httpProxy)},
                {"httpsProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.HttpsProxy, _httpsProxy)},
                {"noProxy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.NoProxy, _noProxy)},
                {"maxIdleConns", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxIdleConns, _maxIdleConns)},
                {"retry", new Dictionary<string, object>
                {
                    {"retryable", runtime.Autoretry},
                    {"maxAttempts", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.MaxAttempts, 3)},
                }},
                {"backoff", new Dictionary<string, object>
                {
                    {"policy", AlibabaCloud.TeaUtil.Common.DefaultString(runtime.BackoffPolicy, "no")},
                    {"period", AlibabaCloud.TeaUtil.Common.DefaultNumber(runtime.BackoffPeriod, 1)},
                }},
                {"ignoreSSL", runtime.IgnoreSSL},
            };

            TeaRequest _lastRequest = null;
            Exception _lastException = null;
            long _now = System.DateTime.Now.Millisecond;
            int _retryTimes = 0;
            while (TeaCore.AllowRetry((IDictionary) runtime_["retry"], _retryTimes, _now))
            {
                if (_retryTimes > 0)
                {
                    int backoffTime = TeaCore.GetBackoffTime((IDictionary)runtime_["backoff"], _retryTimes);
                    if (backoffTime > 0)
                    {
                        TeaCore.Sleep(backoffTime);
                    }
                }
                _retryTimes = _retryTimes + 1;
                try
                {
                    TeaRequest request_ = new TeaRequest();
                    request_.Protocol = AlibabaCloud.TeaUtil.Common.DefaultString(_protocol, protocol);
                    request_.Method = method;
                    request_.Pathname = pathname;
                    request_.Headers = TeaConverter.merge<string>
                    (
                        new Dictionary<string, string>()
                        {
                            {"date", AlibabaCloud.TeaUtil.Common.GetDateUTCString()},
                            {"host", _endpointHost},
                            {"accept", "application/json"},
                            {"x-acs-signature-nonce", AlibabaCloud.TeaUtil.Common.GetNonce()},
                            {"x-acs-signature-method", "HMAC-SHA1"},
                            {"x-acs-signature-version", "1.0"},
                            {"x-acs-version", version},
                        },
                        headers
                    );
                    if (!AlibabaCloud.TeaUtil.Common.IsUnset(body))
                    {
                        request_.Body = TeaCore.BytesReadable(AlibabaCloud.TeaUtil.Common.ToJSONString(body));
                    }
                    if (!AlibabaCloud.TeaUtil.Common.IsUnset(query))
                    {
                        request_.Query = query;
                    }
                    if (!AlibabaCloud.TeaUtil.Common.EqualString(authType, "Anonymous"))
                    {
                        string accessKeyId = await this._credential.GetAccessKeyIdAsync();
                        string accessKeySecret = await this._credential.GetAccessKeySecretAsync();
                        string securityToken = await this._credential.GetSecurityTokenAsync();
                        if (!AlibabaCloud.TeaUtil.Common.Empty(securityToken))
                        {
                            request_.Headers["x-acs-accesskey-id"] = accessKeyId;
                            request_.Headers["x-acs-security-token"] = securityToken;
                        }
                        string stringToSign = AlibabaCloud.ROAUtil.Common.GetStringToSign(request_);
                        request_.Headers["authorization"] = "acs " + accessKeyId + ":" + AlibabaCloud.ROAUtil.Common.GetSignature(stringToSign, accessKeySecret);
                    }
                    _lastRequest = request_;
                    TeaResponse response_ = await TeaCore.DoActionAsync(request_, runtime_);

                    if (AlibabaCloud.TeaUtil.Common.EqualNumber(response_.StatusCode, 204))
                    {
                        return new Dictionary<string, object>
                        {
                            {"headers", response_.Headers},
                        };
                    }
                    object result = AlibabaCloud.TeaUtil.Common.ReadAsJSON(response_.Body);
                    if (AlibabaCloud.TeaUtil.Common.Is4xx(response_.StatusCode) || AlibabaCloud.TeaUtil.Common.Is5xx(response_.StatusCode))
                    {
                        Dictionary<string, object> err = AlibabaCloud.TeaUtil.Common.AssertAsMap(result);
                        throw new TeaException(new Dictionary<string, object>
                        {
                            {"code", Client.DefaultAny(err.Get("Code"), err.Get("code")) + "Error"},
                            {"message", "code: " + response_.StatusCode + ", " + Client.DefaultAny(err.Get("Message"), err.Get("message")) + " requestid: " + Client.DefaultAny(err.Get("RequestId"), err.Get("requestId"))},
                            {"data", err},
                        });
                    }
                    return new Dictionary<string, object>
                    {
                        {"headers", response_.Headers},
                        {"body", result},
                    };
                }
                catch (Exception e)
                {
                    if (TeaCore.IsRetryable(e))
                    {
                        _lastException = e;
                        continue;
                    }
                    throw e;
                }
            }

            throw new TeaUnretryableException(_lastRequest, _lastException);
        }

        public static object DefaultAny(object inputValue, object defaultValue)
        {
            if (AlibabaCloud.TeaUtil.Common.IsUnset(inputValue))
            {
                return defaultValue;
            }
            return inputValue;
        }

        public void CheckConfig(Config config)
        {
            if (AlibabaCloud.TeaUtil.Common.Empty(_endpointRule) && AlibabaCloud.TeaUtil.Common.Empty(config.Endpoint))
            {
                throw new TeaException(new Dictionary<string, string>
                {
                    {"code", "ParameterMissing"},
                    {"message", "'config.endpoint' can not be empty"},
                });
            }
        }

    }
}
