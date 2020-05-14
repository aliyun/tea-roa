# This file is auto-generated, don't edit it. Thanks.
import time

from alibabacloud_credentials.client import Client as alibabacloud_credentials_client_Client
from alibabacloud_tea_roa import models as alibabacloud_tea_roa_models
from alibabacloud_tea_util.client import Client as alibabacloud_tea_util_client_Client
from Tea.exceptions import TeaException as Tea_exceptions_TeaException
from alibabacloud_credentials import models as alibabacloud_credentials_models
from alibabacloud_tea_util import models as alibabacloud_tea_util_models
from Tea.request import TeaRequest as Tea_request_TeaRequest
from Tea.core import TeaCore as Tea_core_TeaCore
from Tea.response import TeaResponse as Tea_response_TeaResponse
from alibabacloud_roa_util.client import Client as alibabacloud_roa_util_client_Client
from Tea.exceptions import UnretryableException as Tea_exceptions_UnretryableException


"""
This is for ROA SDK
"""
class Client:
    def __init__(self, config, _protocol = "", _read_timeout = 0, _connect_timeout = 0, _http_proxy = "", _https_proxy = "", _no_proxy = "", _max_idle_conns = 0, _endpoint_host = "", _network = "", _endpoint_rule = "", _endpoint_map = None, _suffix = "", _product_id = "", _region_id = "", _credential = None):
        """
        Init client with Config
        @param config config contains the necessary information to create a client
        """
        self._protocol = _protocol
        self._read_timeout = _read_timeout
        self._connect_timeout = _connect_timeout
        self._http_proxy = _http_proxy
        self._https_proxy = _https_proxy
        self._no_proxy = _no_proxy
        self._max_idle_conns = _max_idle_conns
        self._endpoint_host = _endpoint_host
        self._network = _network
        self._endpoint_rule = _endpoint_rule
        self._endpoint_map = {}
        self._suffix = _suffix
        self._product_id = _product_id
        self._region_id = _region_id
        self._credential = _credential
        if alibabacloud_tea_util_client_Client.is_unset(config) :
            raise Tea_exceptions_TeaException({
                "code" : "ParameterMissing",
                "message" : "'config' can not be unset"
                })
        if not alibabacloud_tea_util_client_Client.empty(config.access_key_id) and not alibabacloud_tea_util_client_Client.empty(config.access_key_secret) :
            if not alibabacloud_tea_util_client_Client.empty(config.security_token) :
                config.type = "sts"
            else :
                config.type = "access_key"
            credential_config = alibabacloud_credentials_models.Config(
                access_key_id = config.access_key_id,
                type = config.type,
                access_key_secret = config.access_key_secret,
                security_token = config.security_token
            )
            self._credential = alibabacloud_credentials_client_Client(credential_config)
        elif not alibabacloud_tea_util_client_Client.is_unset(config.credential) :
            self._credential = config.credential
        else :
            raise Tea_exceptions_TeaException({
                "code" : "ParameterMissing",
                "message" : "'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset"
                })
        self._region_id = config.region_id
        self._protocol = config.protocol
        self._endpoint_host = config.endpoint
        self._read_timeout = config.read_timeout
        self._connect_timeout = config.connect_timeout
        self._http_proxy = config.http_proxy
        self._https_proxy = config.https_proxy
        self._max_idle_conns = config.max_idle_conns


    def do_request(self, version, protocol, method, auth_type, pathname, query, headers, body, runtime):
        """
        Encapsulate the request and invoke the network
        @param version product version
        @param protocol http or https
        @param method e.g. GET
        @param authType when authType is Anonymous, the signature will not be calculate
        @param pathname pathname of every api
        @param query which contains request params
        @param headers request headers
        @param body content of request
        @param runtime which controls some details of call api, such as retry times
        @return the response
        """
        runtime.validate()
        _runtime = {
            "timeouted" : "retry",
            "readTimeout" : alibabacloud_tea_util_client_Client.default_number(runtime.read_timeout, self._read_timeout),
            "connectTimeout" : alibabacloud_tea_util_client_Client.default_number(runtime.connect_timeout, self._connect_timeout),
            "httpProxy" : alibabacloud_tea_util_client_Client.default_string(runtime.http_proxy, self._http_proxy),
            "httpsProxy" : alibabacloud_tea_util_client_Client.default_string(runtime.https_proxy, self._https_proxy),
            "noProxy" : alibabacloud_tea_util_client_Client.default_string(runtime.no_proxy, self._no_proxy),
            "maxIdleConns" : alibabacloud_tea_util_client_Client.default_number(runtime.max_idle_conns, self._max_idle_conns),
            "retry" : {
                "retryable" : runtime.autoretry,
                "maxAttempts" : alibabacloud_tea_util_client_Client.default_number(runtime.max_attempts, 3)
                },
            "backoff" : {
                "policy" : alibabacloud_tea_util_client_Client.default_string(runtime.backoff_policy, "no"),
                "period" : alibabacloud_tea_util_client_Client.default_number(runtime.backoff_period, 1)
                },
            "ignoreSSL" : runtime.ignore_ssl
            }
        _last_request = None
        _last_exception = None
        _now = time.time()
        _retry_times = 0
        while Tea_core_TeaCore.allow_retry(_runtime["retry"], _retry_times, _now) :
            if _retry_times > 0 :
                _backoff_time = Tea_core_TeaCore.get_backoff_time(_runtime["backoff"], _retry_times)
                if _backoff_time > 0 :
                    Tea_core_TeaCore.sleep(_backoff_time)
            _retry_times = _retry_times + 1
            try :
                _request = Tea_request_TeaRequest()
                _request.protocol = alibabacloud_tea_util_client_Client.default_string(self._protocol, protocol)
                _request.method = method
                _request.pathname = pathname
                _request.headers = Tea_core_TeaCore.merge({
                    "date" : alibabacloud_tea_util_client_Client.get_date_utcstring(),
                    "host" : self._endpoint_host,
                    "accept" : "application/json",
                    "x-acs-signature-nonce" : alibabacloud_tea_util_client_Client.get_nonce(),
                    "x-acs-signature-method" : "HMAC-SHA1",
                    "x-acs-signature-version" : "1.0",
                    "x-acs-version" : version,
                    # user-agent': helper.DEFAULT_UA,
                    # x-sdk-client': helper.DEFAULT_CLIENT
                    }, headers)
                if not alibabacloud_tea_util_client_Client.is_unset(body) :
                    _request.body = alibabacloud_tea_util_client_Client.to_jsonstring(body)
                if not alibabacloud_tea_util_client_Client.is_unset(query) :
                    _request.query = query
                if not alibabacloud_tea_util_client_Client.equal_string(auth_type, "Anonymous") :
                    access_key_id = self._credential.get_access_key_id()
                    access_key_secret = self._credential.get_access_key_secret()
                    security_token = self._credential.get_security_token()
                    if not alibabacloud_tea_util_client_Client.empty(security_token) :
                        _request.headers["x-acs-accesskey-id"] = access_key_id
                        _request.headers["x-acs-security-token"] = security_token
                    string_to_sign = alibabacloud_roa_util_client_Client.get_string_to_sign(_request)
                    _request.headers["authorization"] = "acs " + access_key_id + ":" + alibabacloud_roa_util_client_Client.get_signature(string_to_sign, access_key_secret) + ""
                _last_request = _request
                _response= Tea_core_TeaCore.do_action(_request, _runtime)
                if alibabacloud_tea_util_client_Client.equal_number(_response.status_code, 204) :
                    return {
                        "headers" : _response.headers
                        }
                result = alibabacloud_tea_util_client_Client.read_as_json(_response.body)
                if alibabacloud_tea_util_client_Client.is_4xx(_response.status_code) or alibabacloud_tea_util_client_Client.is_5xx(_response.status_code) :
                    err = alibabacloud_tea_util_client_Client.assert_as_map(result)
                    raise Tea_exceptions_TeaException({
                        "code" : "" + self.default_any(err["Code"], err["code"]) + "Error",
                        "message" : "code: " + _response.status_code + ", " + self.default_any(err["Message"], err["message"]) + " requestid: " + self.default_any(err["RequestId"], err["requestId"]) + "",
                        "data" : err
                        })
                return {
                    "headers" : _response.headers,
                    "body" : result
                    }
            except Exception as e :
                if Tea_core_TeaCore.is_retryable(e) :
                    _last_exception = e
                    continue
                raise e
        raise Tea_exceptions_UnretryableException(_last_request, _last_exception)

    @staticmethod
    def default_any(input_value, default_value):
        """
        If inputValue is not null, return it or return defaultValue
        @param inputValue  users input value
        @param defaultValue default value
        @return the final result
        """
        if alibabacloud_tea_util_client_Client.is_unset(input_value) :
            return default_value
        return input_value

    def check_config(self, config):
        """
        If the endpointRule and config.endpoint are empty, throw error
        @param config config contains the necessary information to create a client
        """
        if alibabacloud_tea_util_client_Client.empty(self._endpoint_rule) and alibabacloud_tea_util_client_Client.empty(config.endpoint) :
            raise Tea_exceptions_TeaException({
                "code" : "ParameterMissing",
                "message" : "'config.endpoint' can not be empty"
                })
