// This file is auto-generated, don't edit it
import Util, * as $Util from '@alicloud/tea-util';
import Credential, * as $Credential from '@alicloud/credentials';
import ROAUtil from '@alicloud/roa-util';
import { Readable } from 'stream';
import * as $tea from '@alicloud/tea-typescript';

export class Config extends $tea.Model {
  accessKeyId?: string;
  accessKeySecret?: string;
  type?: string;
  securityToken?: string;
  protocol?: string;
  regionId?: string;
  readTimeout?: number;
  connectTimeout?: number;
  httpProxy?: string;
  httpsProxy?: string;
  endpoint?: string;
  noProxy?: string;
  maxIdleConns?: number;
  network?: string;
  suffix?: string;
  static names(): { [key: string]: string } {
    return {
      accessKeyId: 'accessKeyId',
      accessKeySecret: 'accessKeySecret',
      type: 'type',
      securityToken: 'securityToken',
      protocol: 'protocol',
      regionId: 'regionId',
      readTimeout: 'readTimeout',
      connectTimeout: 'connectTimeout',
      httpProxy: 'httpProxy',
      httpsProxy: 'httpsProxy',
      endpoint: 'endpoint',
      noProxy: 'noProxy',
      maxIdleConns: 'maxIdleConns',
      network: 'network',
      suffix: 'suffix',
    };
  }

  static types(): { [key: string]: any } {
    return {
      accessKeyId: 'string',
      accessKeySecret: 'string',
      type: 'string',
      securityToken: 'string',
      protocol: 'string',
      regionId: 'string',
      readTimeout: 'number',
      connectTimeout: 'number',
      httpProxy: 'string',
      httpsProxy: 'string',
      endpoint: 'string',
      noProxy: 'string',
      maxIdleConns: 'number',
      network: 'string',
      suffix: 'string',
    };
  }

  constructor(map?: { [key: string]: any }) {
    super(map);
  }
}


export default class Client {
  _protocol: string;
  _readTimeout: number;
  _connectTimeout: number;
  _httpProxy: string;
  _httpsProxy: string;
  _noProxy: string;
  _maxIdleConns: number;
  _endpointHost: string;
  _network: string;
  _endpointRule: string;
  _endpointMap: {[key: string ]: string};
  _suffix: string;
  _productId: string;
  _regionId: string;
  _credential: Credential;

  constructor(config: Config) {
    let credentialConfig : $Credential.Config = null;
    if (Util.isUnset($tea.toMap(config))) {
      config = new Config({ });
      this._credential = new Credential(null);
    } else {
      credentialConfig = new $Credential.Config({
        accessKeyId: config.accessKeyId,
        type: config.type,
        accessKeySecret: config.accessKeySecret,
        securityToken: config.securityToken,
      });
      this._credential = new Credential(credentialConfig);
    }

    this._network = config.network;
    this._regionId = config.regionId;
    this._suffix = config.suffix;
    this._protocol = config.protocol;
    this._endpointHost = config.endpoint;
    this._readTimeout = config.readTimeout;
    this._connectTimeout = config.connectTimeout;
    this._httpProxy = config.httpProxy;
    this._httpsProxy = config.httpsProxy;
    this._maxIdleConns = config.maxIdleConns;
  }

  async doRequest(version: string, protocol: string, method: string, authType: string, pathname: string, query: {[key: string ]: string}, headers: {[key: string ]: string}, body: any, runtime: $Util.RuntimeOptions): Promise<{[key: string]: any}> {
    let _runtime: { [key: string]: any } = {
      timeouted: "retry",
      readTimeout: Util.defaultNumber(runtime.readTimeout, this._readTimeout),
      connectTimeout: Util.defaultNumber(runtime.connectTimeout, this._connectTimeout),
      httpProxy: Util.defaultString(runtime.httpProxy, this._httpProxy),
      httpsProxy: Util.defaultString(runtime.httpsProxy, this._httpsProxy),
      noProxy: Util.defaultString(runtime.noProxy, this._noProxy),
      maxIdleConns: Util.defaultNumber(runtime.maxIdleConns, this._maxIdleConns),
      retry: {
        retryable: runtime.autoretry,
        maxAttempts: Util.defaultNumber(runtime.maxAttempts, 3),
      },
      backoff: {
        policy: Util.defaultString(runtime.backoffPolicy, "no"),
        period: Util.defaultNumber(runtime.backoffPeriod, 1),
      },
      ignoreSSL: runtime.ignoreSSL,
    }

    let _lastRequest = null;
    let _now = Date.now();
    let _retryTimes = 0;
    while ($tea.allowRetry(_runtime['retry'], _retryTimes, _now)) {
      if (_retryTimes > 0) {
        let _backoffTime = $tea.getBackoffTime(_runtime['backoff'], _retryTimes);
        if (_backoffTime > 0) {
          await $tea.sleep(_backoffTime);
        }
      }

      _retryTimes = _retryTimes + 1;
      try {
        let request_ = new $tea.Request();
        request_.protocol = Util.defaultString(this._protocol, protocol);
        request_.method = method;
        request_.pathname = pathname;
        request_.headers = {
          date: Util.getDateUTCString(),
          host: this._endpointHost,
          accept: "application/json",
          'x-acs-signature-nonce': Util.getNonce(),
          'x-acs-signature-method': "HMAC-SHA1",
          'x-acs-signature-version': "1.0",
          'x-acs-version': version,
          // user-agent': helper.DEFAULT_UA,
          // x-sdk-client': helper.DEFAULT_CLIENT
          ...headers,
        };
        if (!Util.isUnset(body)) {
          request_.body = new $tea.BytesReadable(Util.toJSONString(body));
        }

        if (!Util.isUnset(query)) {
          request_.query = query;
        }

        if (!Util.equalString(authType, "Anonymous")) {
          let accessKeyId = await this._credential.getAccessKeyId();
          let accessKeySecret = await this._credential.getAccessKeySecret();
          let securityToken = await this._credential.getSecurityToken();
          if (!Util.empty(securityToken)) {
            request_.headers["x-acs-accesskey-id"] = accessKeyId;
            request_.headers["x-acs-security-token"] = securityToken;
          }

          let stringToSign = ROAUtil.getStringToSign(request_);
          request_.headers["authorization"] = `acs ${accessKeyId}:${ROAUtil.getSignature(stringToSign, accessKeySecret)}`;
        }

        _lastRequest = request_;
        let response_ = await $tea.doAction(request_, _runtime);

        if (Util.equalNumber(response_.statusCode, 204)) {
          return {
            headers: response_.headers,
          };
        }

        let result = await Util.readAsJSON(response_.body);
        if (Util.is4xx(response_.statusCode) || Util.is5xx(response_.statusCode)) {
          let err = Util.assertAsMap(result);
          throw $tea.newError({
            code: `${Client.defaultAny(err["Code"], err["code"])}Error`,
            message: `code: ${response_.statusCode}, ${Client.defaultAny(err["Message"], err["message"])} requestid: ${Client.defaultAny(err["RequestId"], err["requestId"])}`,
            data: err,
          });
        }

        return {
          headers: response_.headers,
          body: result,
        };
      } catch (ex) {
        if ($tea.isRetryable(ex)) {
          continue;
        }
        throw ex;
      }
    }

    throw $tea.newUnretryableError(_lastRequest);
  }

  static defaultAny(inputValue: any, defaultValue: any): any {
    if (Util.isUnset(inputValue)) {
      return defaultValue;
    }

    return inputValue;
  }

  checkConfig(config: Config): void {
    if (Util.empty(this._endpointRule) && Util.empty(config.endpoint)) {
      throw $tea.newError({
        name: "ParameterMissing",
        message: "'config.endpoint' can not be empty",
      });
    }

  }

}
