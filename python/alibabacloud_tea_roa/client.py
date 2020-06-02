# This file is auto-generated, don't edit it. Thanks.
import time

from alibabacloud_credentials.client import Client as CredentialClient
from alibabacloud_tea_roa import models as _roa_models
from alibabacloud_tea_util.client import Client as UtilClient
from Tea.exceptions import TeaException
from alibabacloud_credentials import models as credential_models
from alibabacloud_tea_util import models as util_models
from Tea.request import TeaRequest
from Tea.core import TeaCore
from Tea.response import TeaResponse
from alibabacloud_roa_util.client import Client as ROAUtilClient
from Tea.exceptions import UnretryableException

"""
This is for ROA SDK
"""


class Client:
    def __init__(self, config, _protocol=None, _read_timeout=None, _connect_timeout=None, _http_proxy=None, _https_proxy=None, _no_proxy=None, _max_idle_conns=None, _endpoint_host=None, _network=None, _endpoint_rule=None, _endpoint_map=None, _suffix=None, _product_id=None, _region_id=None, _credential=None):
        """
        Init client with Config
        @param config: config contains the necessary information to create a client
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
        if UtilClient.is_unset(config.to_map()):
            raise TeaException({
                "code": "ParameterMissing",
                "message": "'config' can not be unset"
            })
        if not UtilClient.empty(config.access_key_id) and not UtilClient.empty(config.access_key_secret):
            if not UtilClient.empty(config.security_token):
                config.type = "sts"
            else:
                config.type = "access_key"
            credential_config = credential_models.Config(
                access_key_id=config.access_key_id,
                type=config.type,
                access_key_secret=config.access_key_secret,
                security_token=config.security_token
            )
            self._credential = CredentialClient(credential_config)
        elif not UtilClient.is_unset(config.credential):
            self._credential = config.credential
        else:
            raise TeaException({
                "code": "ParameterMissing",
                "message": "'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset"
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
        @param version: product version
        @param protocol: http or https
        @param method: e.g. GET
        @param auth_type: when authType is Anonymous, the signature will not be calculate
        @param pathname: pathname of every api
        @param query: which contains request params
        @param headers: request headers
        @param body: content of request
        @param runtime: which controls some details of call api, such as retry times
        @return the response
        """
        runtime.validate()
        _runtime = {
            "timeouted": "retry",
            "readTimeout": UtilClient.default_number(runtime.read_timeout, self._read_timeout),
            "connectTimeout": UtilClient.default_number(runtime.connect_timeout, self._connect_timeout),
            "httpProxy": UtilClient.default_string(runtime.http_proxy, self._http_proxy),
            "httpsProxy": UtilClient.default_string(runtime.https_proxy, self._https_proxy),
            "noProxy": UtilClient.default_string(runtime.no_proxy, self._no_proxy),
            "maxIdleConns": UtilClient.default_number(runtime.max_idle_conns, self._max_idle_conns),
            "retry": {
                "retryable": runtime.autoretry,
                "maxAttempts": UtilClient.default_number(runtime.max_attempts, 3)
            },
            "backoff": {
                "policy": UtilClient.default_string(runtime.backoff_policy, "no"),
                "period": UtilClient.default_number(runtime.backoff_period, 1)
            },
            "ignoreSSL": runtime.ignore_ssl
        }
        _last_request = None
        _last_exception = None
        _now = time.time()
        _retry_times = 0
        while TeaCore.allow_retry(_runtime.get('retry'), _retry_times, _now):
            if _retry_times > 0:
                _backoff_time = TeaCore.get_backoff_time(_runtime.get('backoff'), _retry_times)
                if _backoff_time > 0:
                    TeaCore.sleep(_backoff_time)
            _retry_times = _retry_times + 1
            try:
                _request = TeaRequest()
                _request.protocol = UtilClient.default_string(self._protocol, protocol)
                _request.method = method
                _request.pathname = pathname
                _request.headers = TeaCore.merge({
                    "date": UtilClient.get_date_utcstring(),
                    "host": self._endpoint_host,
                    "accept": "application/json",
                    "x-acs-signature-nonce": UtilClient.get_nonce(),
                    "x-acs-signature-method": "HMAC-SHA1",
                    "x-acs-signature-version": "1.0",
                    "x-acs-version": version,
                    # user-agent': helper.DEFAULT_UA,
                    # x-sdk-client': helper.DEFAULT_CLIENT
                }, headers)
                if not UtilClient.is_unset(body):
                    _request.body = UtilClient.to_jsonstring(body)
                if not UtilClient.is_unset(query):
                    _request.query = query
                if not UtilClient.equal_string(auth_type, "Anonymous"):
                    access_key_id = self._credential.get_access_key_id()
                    access_key_secret = self._credential.get_access_key_secret()
                    security_token = self._credential.get_security_token()
                    if not UtilClient.empty(security_token):
                        _request.headers["x-acs-accesskey-id"] = access_key_id
                        _request.headers["x-acs-security-token"] = security_token
                    string_to_sign = ROAUtilClient.get_string_to_sign(_request)
                    _request.headers["authorization"] = "acs " + str(access_key_id) + ":" + str(ROAUtilClient.get_signature(string_to_sign, access_key_secret)) + ""
                _last_request = _request
                _response = TeaCore.do_action(_request, _runtime)
                if UtilClient.equal_number(_response.status_code, 204):
                    return {
                        "headers": _response.headers
                    }
                result = UtilClient.read_as_json(_response.body)
                if UtilClient.is_4xx(_response.status_code) or UtilClient.is_5xx(_response.status_code):
                    err = UtilClient.assert_as_map(result)
                    raise TeaException({
                        "code": "" + str(self.default_any(err.get('Code'), err.get('code'))) + "Error",
                        "message": "code: " + str(_response.status_code) + ", " + str(self.default_any(err.get('Message'), err.get('message'))) + " requestid: " + str(self.default_any(err.get('RequestId'), err.get('requestId'))) + "",
                        "data": err
                    })
                return {
                    "headers": _response.headers,
                    "body": result
                }
            except Exception as e:
                if TeaCore.is_retryable(e):
                    _last_exception = e
                    continue
                raise e
        raise UnretryableException(_last_request, _last_exception)

    @staticmethod
    def default_any(input_value, default_value):
        """
        If inputValue is not null, return it or return defaultValue
        @param input_value:  users input value
        @param default_value: default value
        @return the final result
        """
        if UtilClient.is_unset(input_value):
            return default_value
        return input_value

    def check_config(self, config):
        """
        If the endpointRule and config.endpoint are empty, throw error
        @param config: config contains the necessary information to create a client
        """
        if UtilClient.empty(self._endpoint_rule) and UtilClient.empty(config.endpoint):
            raise TeaException({
                "code": "ParameterMissing",
                "message": "'config.endpoint' can not be empty"
            })
