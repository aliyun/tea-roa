package client

import (
	"fmt"
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"

	util "github.com/alibabacloud-go/tea-utils/service"
	"github.com/alibabacloud-go/tea/tea"
	"github.com/alibabacloud-go/tea/utils"
	credential "github.com/aliyun/credentials-go/credentials"
)

func mockServer(status int, json string) (server *httptest.Server) {
	// Start a test server locally.
	ts := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(status)
		w.Write([]byte(json))
		return
	}))
	return ts
}

func Test_DoRequest(t *testing.T) {
	conf := new(credential.Config)
	conf.AccessKeyId = tea.String("accesskey_id")
	conf.AccessKeySecret = tea.String("accesskey_secret")
	conf.Type = tea.String("access_key")
	c, err := credential.NewCredential(conf)
	utils.AssertNil(t, err)

	config := new(Config).SetRegionId("域名")
	_, err = NewClient(config)
	utils.AssertNotNil(t, err)
	utils.AssertEqual(t, err.Error(), "域名 is not matched ^[a-zA-Z0-9_-]+$")

	_, err = NewClient(nil)
	utils.AssertNotNil(t, err)
	utils.AssertEqual(t, err.Error(), "SDKError:\n   Code: ParameterMissing\n   Message: 'config' can not be unset\n   Data: \n")

	config.SetRegionId("cn-hangzhou")
	_, err = NewClient(config)
	utils.AssertNotNil(t, err)
	utils.AssertEqual(t, err.Error(), "SDKError:\n   Code: ParameterMissing\n   Message: 'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset\n   Data: \n")

	config.SetCredential(c)
	_, err = NewClient(config)
	utils.AssertNil(t, err)

	config.SetAccessKeyId("accesskey_id").
		SetAccessKeySecret("accesskey_secret")
	_, err = NewClient(config)
	utils.AssertNil(t, err)

	config.SetSecurityToken("SecurityToken")
	_, err = NewClient(config)
	utils.AssertNil(t, err)

	utils.AssertEqual(t, fmt.Sprintln(config), fmt.Sprintln(config.GoString()))

	config.SetProtocol("http").
		SetReadTimeout(10).
		SetConnectTimeout(10).
		SetHttpProxy("httpproxy").
		SetHttpsProxy("httpsproxy").
		SetEndpoint("endpoint").
		SetNoProxy("npproxy").
		SetMaxIdleConns(1).
		SetNetwork("public").
		SetSuffix("ali").
		SetType("access_key").
		SetUserAgent("rpc")

	config.SetHttpProxy("").
		SetHttpsProxy("")
	client, err := NewClient(config)
	utils.AssertNil(t, err)
	utils.AssertNotNil(t, client)

	ts := mockServer(400, `{"Code": "杭州"}`)
	defer ts.Close()
	client.EndpointHost = tea.String(strings.Replace(ts.URL, "http://", "", 1))
	runtime := new(util.RuntimeOptions)
	resp, err := client.DoRequest(tea.String("2019-12-12"), tea.String("HTTP"), tea.String("GET"),
		tea.String("AK"), tea.String("/test"), nil, nil, map[string]interface{}{"test": "ok"}, runtime)
	utils.AssertNotNil(t, err)
	utils.AssertEqual(t, err.Error(), "SDKError:\n   Code: 杭州Error\n   Message: code: 400, <nil> requestid: <nil>\n   Data: {\"Code\":\"杭州\"}\n")
	utils.AssertNil(t, resp)

	runtime.SetMaxAttempts(3).SetAutoretry(true).SetBackoffPeriod(1).SetBackoffPolicy("ok")
	resp, err = client.DoRequest(tea.String("2019-12-12"), tea.String("HTTP"), tea.String("GET"),
		tea.String("AK"), tea.String("/test"), nil, nil, map[string]interface{}{"test": "ok"}, runtime)
	utils.AssertNotNil(t, err)
	utils.AssertEqual(t, err.Error(), "SDKError:\n   Code: 杭州Error\n   Message: code: 400, <nil> requestid: <nil>\n   Data: {\"Code\":\"杭州\"}\n")
	utils.AssertNil(t, resp)

	ts = mockServer(200, `{"Code": "杭州"}`)
	client.EndpointHost = tea.String(strings.Replace(ts.URL, "http://", "", 1))
	resp, err = client.DoRequest(tea.String("2019-12-12"), tea.String("HTTP"), tea.String("GET"),
		tea.String("AK"), tea.String("/test"), nil, nil, map[string]interface{}{"test": "ok"}, runtime)
	utils.AssertNil(t, err)
	utils.AssertEqual(t, resp["body"], map[string]interface{}{"Code": "杭州"})

	ts = mockServer(204, ``)
	client.EndpointHost = tea.String(strings.Replace(ts.URL, "http://", "", 1))
	resp, err = client.DoRequest(tea.String("2019-12-12"), tea.String("HTTP"), tea.String("GET"),
		tea.String("AK"), tea.String("/test"), map[string]*string{"test": tea.String("ok")}, nil,
		map[string]interface{}{"test": "ok"}, runtime)
	utils.AssertNil(t, err)
	utils.AssertNil(t, resp["body"])

	err = client.CheckConfig(config)
	utils.AssertNil(t, err)

	config.SetEndpoint("")
	client.EndpointRule = tea.String("")
	err = client.CheckConfig(config)
	utils.AssertEqual(t, err.Error(), "SDKError:\n   Code: ParameterMissing\n   Message: 'config.endpoint' can not be empty\n   Data: \n")
}
