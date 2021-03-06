// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.tearoa.models;

import com.aliyun.tea.*;

/**
 * Model for initing client
 */
public class Config extends TeaModel {
    // accesskey id
    @NameInMap("accessKeyId")
    public String accessKeyId;

    // accesskey secret
    @NameInMap("accessKeySecret")
    public String accessKeySecret;

    // security token
    @NameInMap("securityToken")
    public String securityToken;

    // http protocol
    @NameInMap("protocol")
    public String protocol;

    // region id
    @NameInMap("regionId")
    @Validation(pattern = "^[a-zA-Z0-9_-]+$")
    public String regionId;

    // read timeout
    @NameInMap("readTimeout")
    public Integer readTimeout;

    // connect timeout
    @NameInMap("connectTimeout")
    public Integer connectTimeout;

    // http proxy
    @NameInMap("httpProxy")
    public String httpProxy;

    // https proxy
    @NameInMap("httpsProxy")
    public String httpsProxy;

    // credential
    @NameInMap("credential")
    public com.aliyun.credentials.Client credential;

    // endpoint
    @NameInMap("endpoint")
    public String endpoint;

    // proxy white list
    @NameInMap("noProxy")
    public String noProxy;

    // user agent
    @NameInMap("userAgent")
    public String userAgent;

    // max idle conns
    @NameInMap("maxIdleConns")
    public Integer maxIdleConns;

    // network for endpoint
    @NameInMap("network")
    @Validation(pattern = "^[a-zA-Z0-9_-]+$")
    public String network;

    // suffix for endpoint
    @NameInMap("suffix")
    @Validation(pattern = "^[a-zA-Z0-9_-]+$")
    public String suffix;

    // credential type
    @NameInMap("type")
    @Deprecated
    public String type;

    public static Config build(java.util.Map<String, ?> map) throws Exception {
        Config self = new Config();
        return TeaModel.build(map, self);
    }

    public Config setAccessKeyId(String accessKeyId) {
        this.accessKeyId = accessKeyId;
        return this;
    }

    public String getAccessKeyId() {
        return this.accessKeyId;
    }

    public Config setAccessKeySecret(String accessKeySecret) {
        this.accessKeySecret = accessKeySecret;
        return this;
    }

    public String getAccessKeySecret() {
        return this.accessKeySecret;
    }

    public Config setSecurityToken(String securityToken) {
        this.securityToken = securityToken;
        return this;
    }

    public String getSecurityToken() {
        return this.securityToken;
    }

    public Config setProtocol(String protocol) {
        this.protocol = protocol;
        return this;
    }

    public String getProtocol() {
        return this.protocol;
    }

    public Config setRegionId(String regionId) {
        this.regionId = regionId;
        return this;
    }

    public String getRegionId() {
        return this.regionId;
    }

    public Config setReadTimeout(Integer readTimeout) {
        this.readTimeout = readTimeout;
        return this;
    }

    public Integer getReadTimeout() {
        return this.readTimeout;
    }

    public Config setConnectTimeout(Integer connectTimeout) {
        this.connectTimeout = connectTimeout;
        return this;
    }

    public Integer getConnectTimeout() {
        return this.connectTimeout;
    }

    public Config setHttpProxy(String httpProxy) {
        this.httpProxy = httpProxy;
        return this;
    }

    public String getHttpProxy() {
        return this.httpProxy;
    }

    public Config setHttpsProxy(String httpsProxy) {
        this.httpsProxy = httpsProxy;
        return this;
    }

    public String getHttpsProxy() {
        return this.httpsProxy;
    }

    public Config setCredential(com.aliyun.credentials.Client credential) {
        this.credential = credential;
        return this;
    }

    public com.aliyun.credentials.Client getCredential() {
        return this.credential;
    }

    public Config setEndpoint(String endpoint) {
        this.endpoint = endpoint;
        return this;
    }

    public String getEndpoint() {
        return this.endpoint;
    }

    public Config setNoProxy(String noProxy) {
        this.noProxy = noProxy;
        return this;
    }

    public String getNoProxy() {
        return this.noProxy;
    }

    public Config setUserAgent(String userAgent) {
        this.userAgent = userAgent;
        return this;
    }

    public String getUserAgent() {
        return this.userAgent;
    }

    public Config setMaxIdleConns(Integer maxIdleConns) {
        this.maxIdleConns = maxIdleConns;
        return this;
    }

    public Integer getMaxIdleConns() {
        return this.maxIdleConns;
    }

    public Config setNetwork(String network) {
        this.network = network;
        return this;
    }

    public String getNetwork() {
        return this.network;
    }

    public Config setSuffix(String suffix) {
        this.suffix = suffix;
        return this;
    }

    public String getSuffix() {
        return this.suffix;
    }

    public Config setType(String type) {
        this.type = type;
        return this;
    }

    public String getType() {
        return this.type;
    }

}
