// This file is auto-generated, don't edit it. Thanks.

#ifndef ALIBABACLOUD_ROA_H_
#define ALIBABACLOUD_ROA_H_

#include <alibabacloud/credential.hpp>
#include <alibabacloud/roautil.hpp>
#include <boost/any.hpp>
#include <darabonba/core.hpp>
#include <darabonba/util.hpp>
#include <iostream>
#include <map>

using namespace std;

namespace Alibabacloud_ROA {
class Config : public Darabonba::Model {
protected:
  void _init() {
    _default = {
        {"accessKeyId", boost::any("")},    {"accessKeySecret", boost::any("")},
        {"securityToken", boost::any("")},  {"protocol", boost::any("http")},
        {"regionId", boost::any("")},       {"readTimeout", boost::any("")},
        {"connectTimeout", boost::any("")}, {"httpProxy", boost::any("")},
        {"httpsProxy", boost::any("")},     {"credential", boost::any("")},
        {"endpoint", boost::any("")},       {"noProxy", boost::any("")},
        {"userAgent", boost::any("")},      {"maxIdleConns", boost::any("")},
        {"network", boost::any("")},        {"suffix", boost::any("")},
        {"type", boost::any("")},
    };
  }

public:
  Config() { _init(); };
  explicit Config(const std::map<string, boost::any> &config)
      : Darabonba::Model(config) {
    _init();
  };

  map<string, boost::any> toMap() {
    map<string, boost::any> res;
    if (nullptr != accessKeyId) {
      res["accessKeyId"] = boost::any(*accessKeyId);
    }
    if (nullptr != accessKeySecret) {
      res["accessKeySecret"] = boost::any(*accessKeySecret);
    }
    if (nullptr != securityToken) {
      res["securityToken"] = boost::any(*securityToken);
    }
    if (nullptr != protocol) {
      res["protocol"] = boost::any(*protocol);
    }
    if (nullptr != regionId) {
      res["regionId"] = boost::any(*regionId);
    }
    if (nullptr != readTimeout) {
      res["readTimeout"] = boost::any(*readTimeout);
    }
    if (nullptr != connectTimeout) {
      res["connectTimeout"] = boost::any(*connectTimeout);
    }
    if (nullptr != httpProxy) {
      res["httpProxy"] = boost::any(*httpProxy);
    }
    if (nullptr != httpsProxy) {
      res["httpsProxy"] = boost::any(*httpsProxy);
    }
    if (nullptr != credential) {
    }
    if (nullptr != endpoint) {
      res["endpoint"] = boost::any(*endpoint);
    }
    if (nullptr != noProxy) {
      res["noProxy"] = boost::any(*noProxy);
    }
    if (nullptr != userAgent) {
      res["userAgent"] = boost::any(*userAgent);
    }
    if (nullptr != maxIdleConns) {
      res["maxIdleConns"] = boost::any(*maxIdleConns);
    }
    if (nullptr != network) {
      res["network"] = boost::any(*network);
    }
    if (nullptr != suffix) {
      res["suffix"] = boost::any(*suffix);
    }
    if (nullptr != type) {
      res["type"] = boost::any(*type);
    }
    return res;
  }

  shared_ptr<string> accessKeyId{};
  shared_ptr<string> accessKeySecret{};
  shared_ptr<string> securityToken{};
  shared_ptr<string> protocol{};
  shared_ptr<string> regionId{};
  shared_ptr<int> readTimeout{};
  shared_ptr<int> connectTimeout{};
  shared_ptr<string> httpProxy{};
  shared_ptr<string> httpsProxy{};
  shared_ptr<Alibabacloud_Credential::Client> credential{};
  shared_ptr<string> endpoint{};
  shared_ptr<string> noProxy{};
  shared_ptr<string> userAgent{};
  shared_ptr<int> maxIdleConns{};
  shared_ptr<string> network{};
  shared_ptr<string> suffix{};
  shared_ptr<string> type{};

  ~Config(){};
};
class Client {
public:
  shared_ptr<string> _protocol{};
  shared_ptr<int> _readTimeout{};
  shared_ptr<int> _connectTimeout{};
  shared_ptr<string> _httpProxy{};
  shared_ptr<string> _httpsProxy{};
  shared_ptr<string> _noProxy{};
  shared_ptr<int> _maxIdleConns{};
  shared_ptr<string> _endpointHost{};
  shared_ptr<string> _network{};
  shared_ptr<string> _endpointRule{};
  shared_ptr<map<string, string>> _endpointMap{};
  shared_ptr<string> _suffix{};
  shared_ptr<string> _productId{};
  shared_ptr<string> _regionId{};
  shared_ptr<string> _userAgent{};
  shared_ptr<Alibabacloud_Credential::Client> _credential{};
  explicit Client(shared_ptr<Config> config);
  map<string, boost::any>
  doRequest(shared_ptr<string> version, shared_ptr<string> protocol,
            shared_ptr<string> method, shared_ptr<string> authType,
            shared_ptr<string> pathname, shared_ptr<map<string, string>> query,
            shared_ptr<map<string, string>> headers,
            shared_ptr<boost::any> body,
            shared_ptr<Darabonba_Util::RuntimeOptions> runtime);
  map<string, boost::any>
  doRequestWithAction(shared_ptr<string> action, shared_ptr<string> version,
                      shared_ptr<string> protocol, shared_ptr<string> method,
                      shared_ptr<string> authType, shared_ptr<string> pathname,
                      shared_ptr<map<string, string>> query,
                      shared_ptr<map<string, string>> headers,
                      shared_ptr<boost::any> body,
                      shared_ptr<Darabonba_Util::RuntimeOptions> runtime);
  map<string, boost::any>
  doRequestWithForm(shared_ptr<string> version, shared_ptr<string> protocol,
                    shared_ptr<string> method, shared_ptr<string> authType,
                    shared_ptr<string> pathname,
                    shared_ptr<map<string, string>> query,
                    shared_ptr<map<string, string>> headers,
                    shared_ptr<map<string, boost::any>> body,
                    shared_ptr<Darabonba_Util::RuntimeOptions> runtime);
  static boost::any defaultAny(shared_ptr<boost::any> inputValue,
                               shared_ptr<boost::any> defaultValue);
  void checkConfig(shared_ptr<Config> config);

  ~Client(){};
};
} // namespace Alibabacloud_ROA

#endif
