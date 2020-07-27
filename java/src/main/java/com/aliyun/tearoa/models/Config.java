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

}
