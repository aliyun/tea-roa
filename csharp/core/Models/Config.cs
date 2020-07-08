/**
 * This is for ROA SDK 
 */
// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace AlibabaCloud.ROAClient.Models
{
    /**
     * Model for initing client
     */
    public class Config : TeaModel {
        [NameInMap("accessKeyId")]
        [Validation(Required=false)]
        public string AccessKeyId { get; set; }

        [NameInMap("accessKeySecret")]
        [Validation(Required=false)]
        public string AccessKeySecret { get; set; }

        [NameInMap("securityToken")]
        [Validation(Required=false)]
        public string SecurityToken { get; set; }

        [NameInMap("protocol")]
        [Validation(Required=false)]
        public string Protocol { get; set; }

        [NameInMap("regionId")]
        [Validation(Required=false, Pattern="^[a-zA-Z0-9_-]+$")]
        public string RegionId { get; set; }

        [NameInMap("readTimeout")]
        [Validation(Required=false)]
        public int? ReadTimeout { get; set; }

        [NameInMap("connectTimeout")]
        [Validation(Required=false)]
        public int? ConnectTimeout { get; set; }

        [NameInMap("httpProxy")]
        [Validation(Required=false)]
        public string HttpProxy { get; set; }

        [NameInMap("httpsProxy")]
        [Validation(Required=false)]
        public string HttpsProxy { get; set; }

        [NameInMap("credential")]
        [Validation(Required=false)]
        public Aliyun.Credentials.Client Credential { get; set; }

        [NameInMap("endpoint")]
        [Validation(Required=false)]
        public string Endpoint { get; set; }

        [NameInMap("noProxy")]
        [Validation(Required=false)]
        public string NoProxy { get; set; }

        [NameInMap("maxIdleConns")]
        [Validation(Required=false)]
        public int? MaxIdleConns { get; set; }

        [NameInMap("network")]
        [Validation(Required=false, Pattern="^[a-zA-Z0-9_-]+$")]
        public string Network { get; set; }

        [NameInMap("suffix")]
        [Validation(Required=false, Pattern="^[a-zA-Z0-9_-]+$")]
        public string Suffix { get; set; }

        [NameInMap("type")]
        [Validation(Required=false)]
        [Obsolete]
        public string Type { get; set; }

    }

}
