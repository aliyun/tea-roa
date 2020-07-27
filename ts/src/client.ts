// This file is auto-generated, don't edit it
/**
 * This is for ROA SDK 
 */
import Util, * as $Util from '@alicloud/tea-util';
import Credential, * as $Credential from '@alicloud/credentials';
import ROAUtil from '@alicloud/roa-util';
import * as $tea from '@alicloud/tea-typescript';

/**
 * Model for initing client
 */
export class Config extends $tea.Model {
  accessKeyId?: string;
  accessKeySecret?: string;
  securityToken?: string;
  protocol?: string;
  regionId?: string;
  readTimeout?: number;
  connectTimeout?: number;
  httpProxy?: string;
  httpsProxy?: string;
  credential?: ;
  endpoint?: string;
  noProxy?: string;
  userAgent?: string;
  maxIdleConns?: number;
  network?: string;
  suffix?: string;
  type?: string;
  static names(): { [key: string]: string } {
    return {
      accessKeyId: 'accessKeyId',
      accessKeySecret: 'accessKeySecret',
      securityToken: 'securityToken',
      protocol: 'protocol',
      regionId: 'regionId',
      readTimeout: 'readTimeout',
      connectTimeout: 'connectTimeout',
      httpProxy: 'httpProxy',
      httpsProxy: 'httpsProxy',
      credential: 'credential',
      endpoint: 'endpoint',
      noProxy: 'noProxy',
      userAgent: 'userAgent',
      maxIdleConns: 'maxIdleConns',
      network: 'network',
      suffix: 'suffix',
      type: 'type',
    };
  }

  static types(): { [key: string]: any } {
    return {
      accessKeyId: 'string',
      accessKeySecret: 'string',
      securityToken: 'string',
      protocol: 'string',
      regionId: 'string',
      readTimeout: 'number',
      connectTimeout: 'number',
      httpProxy: 'string',
      httpsProxy: 'string',
      credential: ,
      endpoint: 'string',
      noProxy: 'string',
      userAgent: 'string',
      maxIdleConns: 'number',
      network: 'string',
      suffix: 'string',
      type: 'string',
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
  _userAgent: string;
  _credential: Credential;

  /**
   * Init client with Config
   * @param config config contains the necessary information to create a client
   */
  constructor(config: Config) {
    if (Util.isUnset($tea.toMap(config))) {
      throw $tea.newError({
        code: "ParameterMissing",
        message: "'config' can not be unset",
      });
    }

    Util.validateModel(config);
    if (!Util.empty(config.accessKeyId) && !Util.empty(config.accessKeySecret)) {
      if (!Util.empty(config.securityToken)) {
        config.type = "sts";
      } else {
        config.type = "access_key";
      }

      let credentialConfig = new $Credential.Config({
        accessKeyId: config.accessKeyId,
        type: config.type,
        accessKeySecret: config.accessKeySecret,
        securityToken: config.securityToken,
      });
      this._credential = new Credential(credentialConfig);
    } else if (!Util.isUnset(config.credential)) {
      this._credential = config.credential;
    } else {
      throw $tea.newError({
        code: "ParameterMissing",
        message: "'accessKeyId' and 'accessKeySecret' or 'credential' can not be unset",
      });
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

  /**
   * Encapsulate the request and invoke the network
   * @param version product version
   * @param protocol http or https
   * @param method e.g. GET
   * @param authType when authType is Anonymous, the signature will not be calculate
   * @param pathname pathname of every api
   * @param query which contains request params
   * @param headers request headers
   * @param body content of request
   * @param runtime which controls some details of call api, such as retry times
   * @return the response
   */
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
          'user-agent': Util.getUserAgent(this._userAgent),
          // x-sdk-client': helper.DEFAULT_CLIENT
          ...headers,
        };
        if (!Util.isUnset(body)) {
          request_.body = new $tea.BytesReadable(Util.toJSONString(body));
          request_.headers["content-type"] = "application/json; charset=UTF-8;";
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

  /**
   * Encapsulate the request and invoke the network
   * @param action api name
   * @param version product version
   * @param protocol http or https
   * @param method e.g. GET
   * @param authType when authType is Anonymous, the signature will not be calculate
   * @param pathname pathname of every api
   * @param query which contains request params
   * @param headers request headers
   * @param body content of request
   * @param runtime which controls some details of call api, such as retry times
   * @return the response
   */
  async doRequestWithAction(action: string, version: string, protocol: string, method: string, authType: string, pathname: string, query: {[key: string ]: string}, headers: {[key: string ]: string}, body: any, runtime: $Util.RuntimeOptions): Promise<{[key: string]: any}> {
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
          'x-acs-action': action,
          'user-agent': Util.getUserAgent(this._userAgent),
          // x-sdk-client': helper.DEFAULT_CLIENT
          ...headers,
        };
        if (!Util.isUnset(body)) {
          request_.body = new $tea.BytesReadable(Util.toJSONString(body));
          request_.headers["content-type"] = "application/json; charset=UTF-8;";
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

  /**
   * Encapsulate the request and invoke the network
   * @param version product version
   * @param protocol http or https
   * @param method e.g. GET
   * @param authType when authType is Anonymous, the signature will not be calculate
   * @param pathname pathname of every api
   * @param query which contains request params
   * @param headers request headers
   * @param body content of request
   * @param runtime which controls some details of call api, such as retry times
   * @return the response
   */
  async doRequestWithForm(version: string, protocol: string, method: string, authType: string, pathname: string, query: {[key: string ]: string}, headers: {[key: string ]: string}, body: {[key: string ]: any}, runtime: $Util.RuntimeOptions): Promise<{[key: string]: any}> {
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
          'user-agent': Util.getUserAgent(this._userAgent),
          // x-sdk-client': helper.DEFAULT_CLIENT
          ...headers,
        };
        if (!Util.isUnset(body)) {
          request_.body = new $tea.BytesReadable(ROAUtil.toForm(body));
          request_.headers["content-type"] = "application/x-www-form-urlencoded";
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

  /**
   * If inputValue is not null, return it or return defaultValue
   * @param inputValue  users input value
   * @param defaultValue default value
   * @return the final result
   */
  static defaultAny(inputValue: any, defaultValue: any): any {
    if (Util.isUnset(inputValue)) {
      return defaultValue;
    }

    return inputValue;
  }

  /**
   * If the endpointRule and config.endpoint are empty, throw error
   * @param config config contains the necessary information to create a client
   */
  checkConfig(config: Config): void {
    if (Util.empty(this._endpointRule) && Util.empty(config.endpoint)) {
      throw $tea.newError({
        code: "ParameterMissing",
        message: "'config.endpoint' can not be empty",
      });
    }

  }

}
