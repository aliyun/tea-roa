// This file is auto-generated, don't edit it. Thanks.
package client

import (
	"github.com/alibabacloud-go/tea/tea"
	credential "github.com/aliyun/credentials-go/credentials"
	roautil "github.com/aliyun/tea-roa-util/golang/service"
	util "github.com/aliyun/tea-util/golang/service"
)

type Config struct {
	AccessKeyId     *string `json:"accessKeyId" xml:"accessKeyId"`
	AccessKeySecret *string `json:"accessKeySecret" xml:"accessKeySecret"`
	Type            *string `json:"type" xml:"type"`
	SecurityToken   *string `json:"securityToken" xml:"securityToken"`
	Protocol        *string `json:"protocol" xml:"protocol"`
	ReadTimeout     *int    `json:"readTimeout" xml:"readTimeout"`
	ConnectTimeout  *int    `json:"connectTimeout" xml:"connectTimeout"`
	HttpProxy       *string `json:"httpProxy" xml:"httpProxy"`
	HttpsProxy      *string `json:"httpsProxy" xml:"httpsProxy"`
	Endpoint        *string `json:"endpoint" xml:"endpoint"`
	NoProxy         *string `json:"noProxy" xml:"noProxy"`
	MaxIdleConns    *int    `json:"maxIdleConns" xml:"maxIdleConns"`
}

func (s Config) String() string {
	return tea.Prettify(s)
}

func (s Config) GoString() string {
	return s.String()
}

func (s *Config) SetAccessKeyId(v string) *Config {
	s.AccessKeyId = &v
	return s
}

func (s *Config) SetAccessKeySecret(v string) *Config {
	s.AccessKeySecret = &v
	return s
}

func (s *Config) SetType(v string) *Config {
	s.Type = &v
	return s
}

func (s *Config) SetSecurityToken(v string) *Config {
	s.SecurityToken = &v
	return s
}

func (s *Config) SetProtocol(v string) *Config {
	s.Protocol = &v
	return s
}

func (s *Config) SetReadTimeout(v int) *Config {
	s.ReadTimeout = &v
	return s
}

func (s *Config) SetConnectTimeout(v int) *Config {
	s.ConnectTimeout = &v
	return s
}

func (s *Config) SetHttpProxy(v string) *Config {
	s.HttpProxy = &v
	return s
}

func (s *Config) SetHttpsProxy(v string) *Config {
	s.HttpsProxy = &v
	return s
}

func (s *Config) SetEndpoint(v string) *Config {
	s.Endpoint = &v
	return s
}

func (s *Config) SetNoProxy(v string) *Config {
	s.NoProxy = &v
	return s
}

func (s *Config) SetMaxIdleConns(v int) *Config {
	s.MaxIdleConns = &v
	return s
}

type Client struct {
	Protocol       string
	ReadTimeout    int
	ConnectTimeout int
	HttpProxy      string
	HttpsProxy     string
	NoProxy        string
	MaxIdleConns   int
	EndpointHost   string
	Credential     credential.Credential
}

func NewClient(config *Config) (*Client, error) {
	client := new(Client)
	err := client.Init(config)
	return client, err
}

func (client *Client) Init(config *Config) (_err error) {
	if util.IsUnset(tea.ToMap(config)) {
		_err = tea.NewSDKError(map[string]interface{}{
			"name":    "ParameterMissing",
			"message": "'config' can not be unset",
		})
		return _err
	}

	if util.Empty(tea.StringValue(config.Endpoint)) {
		_err = tea.NewSDKError(map[string]interface{}{
			"name":    "ParameterMissing",
			"message": "'config.endpoint' can not be empty",
		})
		return _err
	}

	if util.Empty(tea.StringValue(config.Type)) {
		config.Type = tea.String("access_key")
	}

	credentialConfig := &credential.Config{
		AccessKeyId:     config.AccessKeyId,
		Type:            config.Type,
		AccessKeySecret: config.AccessKeySecret,
		SecurityToken:   config.SecurityToken,
	}
	client.Credential, _err = credential.NewCredential(credentialConfig)
	if _err != nil {
		return _err
	}

	client.Protocol = tea.StringValue(config.Protocol)
	client.EndpointHost = tea.StringValue(config.Endpoint)
	client.ReadTimeout = tea.IntValue(config.ReadTimeout)
	client.ConnectTimeout = tea.IntValue(config.ConnectTimeout)
	client.HttpProxy = tea.StringValue(config.HttpProxy)
	client.HttpsProxy = tea.StringValue(config.HttpsProxy)
	client.MaxIdleConns = tea.IntValue(config.MaxIdleConns)
	return nil
}

func (client *Client) DoRequest(version string, protocol string, method string, authType string, pathname string, query map[string]string, headers map[string]string, body interface{}, runtime *util.RuntimeOptions) (_result map[string]interface{}, _err error) {
	_err = tea.Validate(runtime)
	if _err != nil {
		return nil, _err
	}
	_runtime := map[string]interface{}{
		"timeouted":      "retry",
		"readTimeout":    util.DefaultNumber(tea.IntValue(runtime.ReadTimeout), client.ReadTimeout),
		"connectTimeout": util.DefaultNumber(tea.IntValue(runtime.ConnectTimeout), client.ConnectTimeout),
		"httpProxy":      util.DefaultString(tea.StringValue(runtime.HttpProxy), client.HttpProxy),
		"httpsProxy":     util.DefaultString(tea.StringValue(runtime.HttpsProxy), client.HttpsProxy),
		"noProxy":        util.DefaultString(tea.StringValue(runtime.NoProxy), client.NoProxy),
		"maxIdleConns":   util.DefaultNumber(tea.IntValue(runtime.MaxIdleConns), client.MaxIdleConns),
		"retry": map[string]interface{}{
			"retryable":   tea.BoolValue(runtime.Autoretry),
			"maxAttempts": util.DefaultNumber(tea.IntValue(runtime.MaxAttempts), 3),
		},
		"backoff": map[string]interface{}{
			"policy": util.DefaultString(tea.StringValue(runtime.BackoffPolicy), "no"),
			"period": util.DefaultNumber(tea.IntValue(runtime.BackoffPeriod), 1),
		},
		"ignoreSSL": tea.BoolValue(runtime.IgnoreSSL),
	}

	_resp := make(map[string]interface{})
	for _retryTimes := 0; tea.AllowRetry(_runtime["retry"], _retryTimes); _retryTimes++ {
		if _retryTimes > 0 {
			_backoffTime := tea.GetBackoffTime(_runtime["backoff"], _retryTimes)
			if _backoffTime > 0 {
				tea.Sleep(_backoffTime)
			}
		}

		_resp, _err = func() (map[string]interface{}, error) {
			request_ := tea.NewRequest()
			request_.Protocol = util.DefaultString(client.Protocol, protocol)
			request_.Method = method
			request_.Pathname = pathname
			request_.Headers = tea.Merge(map[string]string{
				"date":                    util.GetDateUTCString(),
				"host":                    client.EndpointHost,
				"accept":                  "application/json",
				"x-acs-signature-nonce":   util.GetNonce(),
				"x-acs-signature-method":  "HMAC-SHA1",
				"x-acs-signature-version": "1.0",
				"x-acs-version":           version,
				// user-agent': helper.DEFAULT_UA,
				// x-sdk-client': helper.DEFAULT_CLIENT
			}, headers)
			if !util.IsUnset(body) {
				request_.Body = tea.ToReader(util.ToJSONString(body))
			}

			if !util.IsUnset(query) {
				request_.Query = query
			}

			if !util.EqualString(authType, "Anonymous") {
				accessKeyId, _err := client.Credential.GetAccessKeyId()
				if _err != nil {
					return nil, _err
				}

				accessKeySecret, _err := client.Credential.GetAccessKeySecret()
				if _err != nil {
					return nil, _err
				}

				securityToken, _err := client.Credential.GetSecurityToken()
				if _err != nil {
					return nil, _err
				}

				if !util.Empty(securityToken) {
					request_.Headers["x-acs-accesskey-id"] = accessKeyId
					request_.Headers["x-acs-security-token"] = securityToken
				}

				stringToSign := roautil.GetStringToSign(request_)
				request_.Headers["authorization"] = "acs " + tea.ToString(accessKeyId) + ":" + tea.ToString(roautil.GetSignature(stringToSign, accessKeySecret))
			}

			response_, _err := tea.DoRequest(request_, _runtime)
			if _err != nil {
				return nil, _err
			}
			if util.EqualNumber(response_.StatusCode, 204) {
				_result = make(map[string]interface{})
				_err = tea.Convert(map[string]map[string]string{
					"headers": response_.Headers,
				}, &_result)
				return _result, _err
			}

			result, _err := util.ReadAsJSON(response_.Body)
			if _err != nil {
				return nil, _err
			}

			if util.Is4xx(response_.StatusCode) || util.Is5xx(response_.StatusCode) {
				err := util.AssertAsMap(result)
				_err = tea.NewSDKError(map[string]interface{}{
					"message": "code: " + tea.ToString(response_.StatusCode) + ", " + tea.ToString(DefaultAny(err["Message"], err["message"])) + " requestid: " + tea.ToString(DefaultAny(err["RequestId"], err["requestId"])),
					"name":    tea.ToString(DefaultAny(err["Code"], err["code"])) + "Error",
				})
				return nil, _err
			}

			_result = make(map[string]interface{})
			_err = tea.Convert(map[string]interface{}{
				"headers": response_.Headers,
				"body":    result,
			}, &_result)
			return _result, _err
		}()
		if !tea.Retryable(_err) {
			break
		}
	}

	return _resp, _err
}

func DefaultAny(inputValue interface{}, defaultValue interface{}) (_result interface{}) {
	if util.IsUnset(inputValue) {
		_result = defaultValue
		return _result
	}

	_result = inputValue
	return _result
}
