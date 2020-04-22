// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.tearoa.models;

import com.aliyun.tea.*;

public class Config extends TeaModel {
    @NameInMap("accessKeyId")
    public String accessKeyId;

    @NameInMap("accessKeySecret")
    public String accessKeySecret;

    @NameInMap("securityToken")
    public String securityToken;

    @NameInMap("protocol")
    public String protocol;

    @NameInMap("regionId")
    public String regionId;

    @NameInMap("readTimeout")
    public Integer readTimeout;

    @NameInMap("connectTimeout")
    public Integer connectTimeout;

    @NameInMap("httpProxy")
    public String httpProxy;

    @NameInMap("httpsProxy")
    public String httpsProxy;

    @NameInMap("credential")
    public com.aliyun.credentials.Client credential;

    @NameInMap("endpoint")
    public String endpoint;

    @NameInMap("noProxy")
    public String noProxy;

    @NameInMap("maxIdleConns")
    public Integer maxIdleConns;

    @NameInMap("network")
    public String network;

    @NameInMap("suffix")
    public String suffix;

    @NameInMap("type")
    @Deprecated
    public String type;

    public static Config build(java.util.Map<String, ?> map) throws Exception {
        Config self = new Config();
        return TeaModel.build(map, self);
    }

}
