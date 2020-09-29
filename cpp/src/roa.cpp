// This file is auto-generated, don't edit it. Thanks.

#include <alibabacloud/credential.hpp>
#include <alibabacloud/roa.hpp>
#include <alibabacloud/roautil.hpp>
#include <boost/any.hpp>
#include <boost/lexical_cast.hpp>
#include <boost/throw_exception.hpp>
#include <darabonba/core.hpp>
#include <darabonba/util.hpp>
#include <iostream>
#include <map>

using namespace std;

Alibabacloud_ROA::Client::Client(shared_ptr<Config> config) {
  if (Darabonba_Util::Client::isUnset(config)) {
    BOOST_THROW_EXCEPTION(Darabonba::Error(
        map<string, string>({{"code", "ParameterMissing"},
                             {"message", "'config' can not be unset"}})));
  }
  Darabonba_Util::Client::validateModel(config);
  if (!Darabonba_Util::Client::empty(config->accessKeyId) &&
      !Darabonba_Util::Client::empty(config->accessKeySecret)) {
    if (!Darabonba_Util::Client::empty(config->securityToken)) {
      config->type = shared_ptr<string>(new string("sts"));
    } else {
      config->type = shared_ptr<string>(new string("access_key"));
    }
    Alibabacloud_Credential::Config credentialConfig =
        Alibabacloud_Credential::Config(
            shared_ptr<map<string, string>>(new map<string, string>(
                {{"accessKeyId",
                  config->accessKeyId == nullptr ? NULL : *config->accessKeyId},
                 {"type", config->type == nullptr ? NULL : *config->type},
                 {"accessKeySecret", config->accessKeySecret == nullptr
                                         ? NULL
                                         : *config->accessKeySecret},
                 {"securityToken", config->securityToken == nullptr
                                       ? NULL
                                       : *config->securityToken}})));
    _credential = shared_ptr<Alibabacloud_Credential::Client>(
        new Alibabacloud_Credential::Client(Alibabacloud_Credential::Client(
            shared_ptr<Alibabacloud_Credential::Config>(
                new Alibabacloud_Credential::Config(credentialConfig)))));
  } else if (!Darabonba_Util::Client::isUnset(config->credential)) {
    _credential = config->credential;
  } else {
    BOOST_THROW_EXCEPTION(Darabonba::Error(map<string, string>(
        {{"code", "ParameterMissing"},
         {"message", "'accessKeyId' and 'accessKeySecret' or 'credential' can "
                     "not be unset"}})));
  }
  _regionId = config->regionId;
  _protocol = config->protocol;
  _endpointHost = config->endpoint;
  _readTimeout = config->readTimeout;
  _connectTimeout = config->connectTimeout;
  _httpProxy = config->httpProxy;
  _httpsProxy = config->httpsProxy;
  _maxIdleConns = config->maxIdleConns;
};

map<string, boost::any> Alibabacloud_ROA::Client::doRequest(
    shared_ptr<string> version, shared_ptr<string> protocol,
    shared_ptr<string> method, shared_ptr<string> authType,
    shared_ptr<string> pathname, shared_ptr<map<string, string>> query,
    shared_ptr<map<string, string>> headers, shared_ptr<boost::any> body,
    shared_ptr<Darabonba_Util::RuntimeOptions> runtime) {
  runtime->validate();
  map<string, boost::any> runtime_ = {
      {"timeouted", boost::any("retry")},
      {"readTimeout", boost::any(Darabonba_Util::Client::defaultNumber(
                          runtime->readTimeout, _readTimeout))},
      {"connectTimeout", boost::any(Darabonba_Util::Client::defaultNumber(
                             runtime->connectTimeout, _connectTimeout))},
      {"httpProxy", boost::any(Darabonba_Util::Client::defaultString(
                        runtime->httpProxy, _httpProxy))},
      {"httpsProxy", boost::any(Darabonba_Util::Client::defaultString(
                         runtime->httpsProxy, _httpsProxy))},
      {"noProxy", boost::any(Darabonba_Util::Client::defaultString(
                      runtime->noProxy, _noProxy))},
      {"maxIdleConns", boost::any(Darabonba_Util::Client::defaultNumber(
                           runtime->maxIdleConns, _maxIdleConns))},
      {"retry",
       boost::any(map<string, boost::any>(
           {{"retryable",
             boost::any(runtime->autoretry == nullptr ? NULL
                                                      : *runtime->autoretry)},
            {"maxAttempts",
             boost::any(Darabonba_Util::Client::defaultNumber(
                 runtime->maxAttempts, shared_ptr<int>(new int(3))))}}))},
      {"backoff",
       boost::any(map<string, boost::any>(
           {{"policy", boost::any(Darabonba_Util::Client::defaultString(
                           runtime->backoffPolicy,
                           shared_ptr<string>(new string("no"))))},
            {"period",
             boost::any(Darabonba_Util::Client::defaultNumber(
                 runtime->backoffPeriod, shared_ptr<int>(new int(1))))}}))},
      {"ignoreSSL",
       boost::any(runtime->ignoreSSL == nullptr ? NULL : *runtime->ignoreSSL)}};
  Darabonba::Request _lastRequest;
  std::exception _lastException;
  int _now = 0;
  int _retryTimes = 0;
  while (Darabonba::Core::allowRetry(
      shared_ptr<boost::any>(new boost::any(runtime_["retry"])),
      shared_ptr<int>(new int(_retryTimes)), shared_ptr<int>(new int(_now)))) {
    if (_retryTimes > 0) {
      int _backoffTime = Darabonba::Core::getBackoffTime(
          shared_ptr<boost::any>(new boost::any(runtime_["backoff"])),
          shared_ptr<int>(new int(_retryTimes)));
      if (_backoffTime > 0) {
        Darabonba::Core::sleep(shared_ptr<int>(new int(_backoffTime)));
      }
    }
    _retryTimes = _retryTimes + 1;
    try {
      Darabonba::Request request_ = Darabonba::Request();
      request_.protocol =
          Darabonba_Util::Client::defaultString(_protocol, protocol);
      request_.method = *method;
      request_.pathname = *pathname;
      request_.headers = Darabonba::Converter::merge(
          map<string, string>({
              {"date", Darabonba_Util::Client::getDateUTCString()},
              {"host", _endpointHost == nullptr ? NULL : *_endpointHost},
              {"accept", "application/json"},
              {"x-acs-signature-nonce", Darabonba_Util::Client::getNonce()},
              {"x-acs-signature-method", "HMAC-SHA1"},
              {"x-acs-signature-version", "1.0"},
              {"x-acs-version", version == nullptr ? NULL : *version},
              {"user-agent", Darabonba_Util::Client::getUserAgent(_userAgent)},
              // x-sdk-client': helper.DEFAULT_CLIENT
          }),
          *headers);
      if (!Darabonba_Util::Client::isUnset(body)) {
        request_.body = Darabonba_Util::Client::toJSONString(body);
        request_.headers.insert(pair<string, string>(
            "content-type", "application/json; charset=utf-8"));
      }
      if (!Darabonba_Util::Client::isUnset(query)) {
        request_.query = *query;
      }
      if (!Darabonba_Util::Client::equalString(
              authType, shared_ptr<string>(new string("Anonymous")))) {
        string accessKeyId = _credential->getAccessKeyId();
        string accessKeySecret = _credential->getAccessKeySecret();
        string securityToken = _credential->getSecurityToken();
        if (!Darabonba_Util::Client::empty(
                shared_ptr<string>(new string(securityToken)))) {
          request_.headers.insert(
              pair<string, string>("x-acs-accesskey-id", accessKeyId));
          request_.headers.insert(
              pair<string, string>("x-acs-security-token", securityToken));
        }
        string stringToSign = Alibabacloud_ROAUtil::Client::getStringToSign(
            shared_ptr<Darabonba::Request>(
                new Darabonba::Request(Darabonba::Request request_)));
        request_.headers.insert(pair<string, string>(
            "authorization",
            string("acs " + accessKeyId + ":" +
                   Alibabacloud_ROAUtil::Client::getSignature(
                       shared_ptr<string>(new string(stringToSign)),
                       shared_ptr<string>(new string(accessKeySecret))) +
                   "")));
      }
      _lastRequest = request_;
      Darabonba::Response response_ =
          Darabonba::Core::doAction(request_, runtime_);
      if (Darabonba_Util::Client::equalNumber(
              shared_ptr<int>(new int(response_.statusCode)),
              shared_ptr<int>(new int(204)))) {
        return {{"headers", response_.headers}};
      }
      boost::any result = Darabonba_Util::Client::readAsJSON(
          shared_ptr<istream>(new istream(response_.body)));
      if (Darabonba_Util::Client::is4xx(
              shared_ptr<int>(new int(response_.statusCode))) ||
          Darabonba_Util::Client::is5xx(
              shared_ptr<int>(new int(response_.statusCode)))) {
        map<string, boost::any> err = Darabonba_Util::Client::assertAsMap(
            shared_ptr<boost::any>(new boost::any(result)));
        BOOST_THROW_EXCEPTION(Darabonba::Error(map<string, boost::any>(
            {{"code",
              boost::any(string(
                  "" +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["Code"])),
                      shared_ptr<boost::any>(new boost::any(err["code"])))) +
                  ""))},
             {"message",
              boost::any(string(
                  "code: " + boost::lexical_cast<string>(response_.statusCode) +
                  ", " +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["Message"])),
                      shared_ptr<boost::any>(new boost::any(err["message"])))) +
                  " request id: " +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["RequestId"])),
                      shared_ptr<boost::any>(
                          new boost::any(err["requestId"])))) +
                  ""))},
             {"data", boost::any(err)}})));
      }
      return {{"headers", boost::any(response_.headers)},
              {"body", boost::any(result)}};
    } catch (std::exception &e) {
      if (Darabonba::Core::isRetryable(e)) {
        _lastException = e;
        continue;
      }
      BOOST_THROW_EXCEPTION(e);
    }
  }
  BOOST_THROW_EXCEPTION(
      Darabonba::UnretryableError(_lastRequest, _lastException));
}

map<string, boost::any> Alibabacloud_ROA::Client::doRequestWithAction(
    shared_ptr<string> action, shared_ptr<string> version,
    shared_ptr<string> protocol, shared_ptr<string> method,
    shared_ptr<string> authType, shared_ptr<string> pathname,
    shared_ptr<map<string, string>> query,
    shared_ptr<map<string, string>> headers, shared_ptr<boost::any> body,
    shared_ptr<Darabonba_Util::RuntimeOptions> runtime) {
  runtime->validate();
  map<string, boost::any> runtime_ = {
      {"timeouted", boost::any("retry")},
      {"readTimeout", boost::any(Darabonba_Util::Client::defaultNumber(
                          runtime->readTimeout, _readTimeout))},
      {"connectTimeout", boost::any(Darabonba_Util::Client::defaultNumber(
                             runtime->connectTimeout, _connectTimeout))},
      {"httpProxy", boost::any(Darabonba_Util::Client::defaultString(
                        runtime->httpProxy, _httpProxy))},
      {"httpsProxy", boost::any(Darabonba_Util::Client::defaultString(
                         runtime->httpsProxy, _httpsProxy))},
      {"noProxy", boost::any(Darabonba_Util::Client::defaultString(
                      runtime->noProxy, _noProxy))},
      {"maxIdleConns", boost::any(Darabonba_Util::Client::defaultNumber(
                           runtime->maxIdleConns, _maxIdleConns))},
      {"retry",
       boost::any(map<string, boost::any>(
           {{"retryable",
             boost::any(runtime->autoretry == nullptr ? NULL
                                                      : *runtime->autoretry)},
            {"maxAttempts",
             boost::any(Darabonba_Util::Client::defaultNumber(
                 runtime->maxAttempts, shared_ptr<int>(new int(3))))}}))},
      {"backoff",
       boost::any(map<string, boost::any>(
           {{"policy", boost::any(Darabonba_Util::Client::defaultString(
                           runtime->backoffPolicy,
                           shared_ptr<string>(new string("no"))))},
            {"period",
             boost::any(Darabonba_Util::Client::defaultNumber(
                 runtime->backoffPeriod, shared_ptr<int>(new int(1))))}}))},
      {"ignoreSSL",
       boost::any(runtime->ignoreSSL == nullptr ? NULL : *runtime->ignoreSSL)}};
  Darabonba::Request _lastRequest;
  std::exception _lastException;
  int _now = 0;
  int _retryTimes = 0;
  while (Darabonba::Core::allowRetry(
      shared_ptr<boost::any>(new boost::any(runtime_["retry"])),
      shared_ptr<int>(new int(_retryTimes)), shared_ptr<int>(new int(_now)))) {
    if (_retryTimes > 0) {
      int _backoffTime = Darabonba::Core::getBackoffTime(
          shared_ptr<boost::any>(new boost::any(runtime_["backoff"])),
          shared_ptr<int>(new int(_retryTimes)));
      if (_backoffTime > 0) {
        Darabonba::Core::sleep(shared_ptr<int>(new int(_backoffTime)));
      }
    }
    _retryTimes = _retryTimes + 1;
    try {
      Darabonba::Request request_ = Darabonba::Request();
      request_.protocol =
          Darabonba_Util::Client::defaultString(_protocol, protocol);
      request_.method = *method;
      request_.pathname = *pathname;
      request_.headers = Darabonba::Converter::merge(
          map<string, string>({
              {"date", Darabonba_Util::Client::getDateUTCString()},
              {"host", _endpointHost == nullptr ? NULL : *_endpointHost},
              {"accept", "application/json"},
              {"x-acs-signature-nonce", Darabonba_Util::Client::getNonce()},
              {"x-acs-signature-method", "HMAC-SHA1"},
              {"x-acs-signature-version", "1.0"},
              {"x-acs-version", version == nullptr ? NULL : *version},
              {"x-acs-action", action == nullptr ? NULL : *action},
              {"user-agent", Darabonba_Util::Client::getUserAgent(_userAgent)},
              // x-sdk-client': helper.DEFAULT_CLIENT
          }),
          *headers);
      if (!Darabonba_Util::Client::isUnset(body)) {
        request_.body = Darabonba_Util::Client::toJSONString(body);
        request_.headers.insert(pair<string, string>(
            "content-type", "application/json; charset=utf-8"));
      }
      if (!Darabonba_Util::Client::isUnset(query)) {
        request_.query = *query;
      }
      if (!Darabonba_Util::Client::equalString(
              authType, shared_ptr<string>(new string("Anonymous")))) {
        string accessKeyId = _credential->getAccessKeyId();
        string accessKeySecret = _credential->getAccessKeySecret();
        string securityToken = _credential->getSecurityToken();
        if (!Darabonba_Util::Client::empty(
                shared_ptr<string>(new string(securityToken)))) {
          request_.headers.insert(
              pair<string, string>("x-acs-accesskey-id", accessKeyId));
          request_.headers.insert(
              pair<string, string>("x-acs-security-token", securityToken));
        }
        string stringToSign = Alibabacloud_ROAUtil::Client::getStringToSign(
            shared_ptr<Darabonba::Request>(
                new Darabonba::Request(Darabonba::Request request_)));
        request_.headers.insert(pair<string, string>(
            "authorization",
            string("acs " + accessKeyId + ":" +
                   Alibabacloud_ROAUtil::Client::getSignature(
                       shared_ptr<string>(new string(stringToSign)),
                       shared_ptr<string>(new string(accessKeySecret))) +
                   "")));
      }
      _lastRequest = request_;
      Darabonba::Response response_ =
          Darabonba::Core::doAction(request_, runtime_);
      if (Darabonba_Util::Client::equalNumber(
              shared_ptr<int>(new int(response_.statusCode)),
              shared_ptr<int>(new int(204)))) {
        return {{"headers", response_.headers}};
      }
      boost::any result = Darabonba_Util::Client::readAsJSON(
          shared_ptr<istream>(new istream(response_.body)));
      if (Darabonba_Util::Client::is4xx(
              shared_ptr<int>(new int(response_.statusCode))) ||
          Darabonba_Util::Client::is5xx(
              shared_ptr<int>(new int(response_.statusCode)))) {
        map<string, boost::any> err = Darabonba_Util::Client::assertAsMap(
            shared_ptr<boost::any>(new boost::any(result)));
        BOOST_THROW_EXCEPTION(Darabonba::Error(map<string, boost::any>(
            {{"code",
              boost::any(string(
                  "" +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["Code"])),
                      shared_ptr<boost::any>(new boost::any(err["code"])))) +
                  ""))},
             {"message",
              boost::any(string(
                  "code: " + boost::lexical_cast<string>(response_.statusCode) +
                  ", " +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["Message"])),
                      shared_ptr<boost::any>(new boost::any(err["message"])))) +
                  " request id: " +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["RequestId"])),
                      shared_ptr<boost::any>(
                          new boost::any(err["requestId"])))) +
                  ""))},
             {"data", boost::any(err)}})));
      }
      return {{"headers", boost::any(response_.headers)},
              {"body", boost::any(result)}};
    } catch (std::exception &e) {
      if (Darabonba::Core::isRetryable(e)) {
        _lastException = e;
        continue;
      }
      BOOST_THROW_EXCEPTION(e);
    }
  }
  BOOST_THROW_EXCEPTION(
      Darabonba::UnretryableError(_lastRequest, _lastException));
}

map<string, boost::any> Alibabacloud_ROA::Client::doRequestWithForm(
    shared_ptr<string> version, shared_ptr<string> protocol,
    shared_ptr<string> method, shared_ptr<string> authType,
    shared_ptr<string> pathname, shared_ptr<map<string, string>> query,
    shared_ptr<map<string, string>> headers,
    shared_ptr<map<string, boost::any>> body,
    shared_ptr<Darabonba_Util::RuntimeOptions> runtime) {
  runtime->validate();
  map<string, boost::any> runtime_ = {
      {"timeouted", boost::any("retry")},
      {"readTimeout", boost::any(Darabonba_Util::Client::defaultNumber(
                          runtime->readTimeout, _readTimeout))},
      {"connectTimeout", boost::any(Darabonba_Util::Client::defaultNumber(
                             runtime->connectTimeout, _connectTimeout))},
      {"httpProxy", boost::any(Darabonba_Util::Client::defaultString(
                        runtime->httpProxy, _httpProxy))},
      {"httpsProxy", boost::any(Darabonba_Util::Client::defaultString(
                         runtime->httpsProxy, _httpsProxy))},
      {"noProxy", boost::any(Darabonba_Util::Client::defaultString(
                      runtime->noProxy, _noProxy))},
      {"maxIdleConns", boost::any(Darabonba_Util::Client::defaultNumber(
                           runtime->maxIdleConns, _maxIdleConns))},
      {"retry",
       boost::any(map<string, boost::any>(
           {{"retryable",
             boost::any(runtime->autoretry == nullptr ? NULL
                                                      : *runtime->autoretry)},
            {"maxAttempts",
             boost::any(Darabonba_Util::Client::defaultNumber(
                 runtime->maxAttempts, shared_ptr<int>(new int(3))))}}))},
      {"backoff",
       boost::any(map<string, boost::any>(
           {{"policy", boost::any(Darabonba_Util::Client::defaultString(
                           runtime->backoffPolicy,
                           shared_ptr<string>(new string("no"))))},
            {"period",
             boost::any(Darabonba_Util::Client::defaultNumber(
                 runtime->backoffPeriod, shared_ptr<int>(new int(1))))}}))},
      {"ignoreSSL",
       boost::any(runtime->ignoreSSL == nullptr ? NULL : *runtime->ignoreSSL)}};
  Darabonba::Request _lastRequest;
  std::exception _lastException;
  int _now = 0;
  int _retryTimes = 0;
  while (Darabonba::Core::allowRetry(
      shared_ptr<boost::any>(new boost::any(runtime_["retry"])),
      shared_ptr<int>(new int(_retryTimes)), shared_ptr<int>(new int(_now)))) {
    if (_retryTimes > 0) {
      int _backoffTime = Darabonba::Core::getBackoffTime(
          shared_ptr<boost::any>(new boost::any(runtime_["backoff"])),
          shared_ptr<int>(new int(_retryTimes)));
      if (_backoffTime > 0) {
        Darabonba::Core::sleep(shared_ptr<int>(new int(_backoffTime)));
      }
    }
    _retryTimes = _retryTimes + 1;
    try {
      Darabonba::Request request_ = Darabonba::Request();
      request_.protocol =
          Darabonba_Util::Client::defaultString(_protocol, protocol);
      request_.method = *method;
      request_.pathname = *pathname;
      request_.headers = Darabonba::Converter::merge(
          map<string, string>({
              {"date", Darabonba_Util::Client::getDateUTCString()},
              {"host", _endpointHost == nullptr ? NULL : *_endpointHost},
              {"accept", "application/json"},
              {"x-acs-signature-nonce", Darabonba_Util::Client::getNonce()},
              {"x-acs-signature-method", "HMAC-SHA1"},
              {"x-acs-signature-version", "1.0"},
              {"x-acs-version", version == nullptr ? NULL : *version},
              {"user-agent", Darabonba_Util::Client::getUserAgent(_userAgent)},
              // x-sdk-client': helper.DEFAULT_CLIENT
          }),
          *headers);
      if (!Darabonba_Util::Client::isUnset(body)) {
        request_.body = Alibabacloud_ROAUtil::Client::toForm(body);
        request_.headers.insert(pair<string, string>(
            "content-type", "application/x-www-form-urlencoded"));
      }
      if (!Darabonba_Util::Client::isUnset(query)) {
        request_.query = *query;
      }
      if (!Darabonba_Util::Client::equalString(
              authType, shared_ptr<string>(new string("Anonymous")))) {
        string accessKeyId = _credential->getAccessKeyId();
        string accessKeySecret = _credential->getAccessKeySecret();
        string securityToken = _credential->getSecurityToken();
        if (!Darabonba_Util::Client::empty(
                shared_ptr<string>(new string(securityToken)))) {
          request_.headers.insert(
              pair<string, string>("x-acs-accesskey-id", accessKeyId));
          request_.headers.insert(
              pair<string, string>("x-acs-security-token", securityToken));
        }
        string stringToSign = Alibabacloud_ROAUtil::Client::getStringToSign(
            shared_ptr<Darabonba::Request>(
                new Darabonba::Request(Darabonba::Request request_)));
        request_.headers.insert(pair<string, string>(
            "authorization",
            string("acs " + accessKeyId + ":" +
                   Alibabacloud_ROAUtil::Client::getSignature(
                       shared_ptr<string>(new string(stringToSign)),
                       shared_ptr<string>(new string(accessKeySecret))) +
                   "")));
      }
      _lastRequest = request_;
      Darabonba::Response response_ =
          Darabonba::Core::doAction(request_, runtime_);
      if (Darabonba_Util::Client::equalNumber(
              shared_ptr<int>(new int(response_.statusCode)),
              shared_ptr<int>(new int(204)))) {
        return {{"headers", response_.headers}};
      }
      boost::any result = Darabonba_Util::Client::readAsJSON(
          shared_ptr<istream>(new istream(response_.body)));
      if (Darabonba_Util::Client::is4xx(
              shared_ptr<int>(new int(response_.statusCode))) ||
          Darabonba_Util::Client::is5xx(
              shared_ptr<int>(new int(response_.statusCode)))) {
        map<string, boost::any> err = Darabonba_Util::Client::assertAsMap(
            shared_ptr<boost::any>(new boost::any(result)));
        BOOST_THROW_EXCEPTION(Darabonba::Error(map<string, boost::any>(
            {{"code",
              boost::any(string(
                  "" +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["Code"])),
                      shared_ptr<boost::any>(new boost::any(err["code"])))) +
                  ""))},
             {"message",
              boost::any(string(
                  "code: " + boost::lexical_cast<string>(response_.statusCode) +
                  ", " +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["Message"])),
                      shared_ptr<boost::any>(new boost::any(err["message"])))) +
                  " request id: " +
                  Darabonba::Converter::toString(Client::defaultAny(
                      shared_ptr<boost::any>(new boost::any(err["RequestId"])),
                      shared_ptr<boost::any>(
                          new boost::any(err["requestId"])))) +
                  ""))},
             {"data", boost::any(err)}})));
      }
      return {{"headers", boost::any(response_.headers)},
              {"body", boost::any(result)}};
    } catch (std::exception &e) {
      if (Darabonba::Core::isRetryable(e)) {
        _lastException = e;
        continue;
      }
      BOOST_THROW_EXCEPTION(e);
    }
  }
  BOOST_THROW_EXCEPTION(
      Darabonba::UnretryableError(_lastRequest, _lastException));
}

boost::any
Alibabacloud_ROA::Client::defaultAny(shared_ptr<boost::any> inputValue,
                                     shared_ptr<boost::any> defaultValue) {
  if (Darabonba_Util::Client::isUnset(inputValue)) {
    return *defaultValue;
  }
  return *inputValue;
}

void Alibabacloud_ROA::Client::checkConfig(shared_ptr<Config> config) {
  if (Darabonba_Util::Client::empty(_endpointRule) &&
      Darabonba_Util::Client::empty(config->endpoint)) {
    BOOST_THROW_EXCEPTION(Darabonba::Error(map<string, string>(
        {{"code", "ParameterMissing"},
         {"message", "'config.endpoint' can not be empty"}})));
  }
}
