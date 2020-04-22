<?php

// This file is auto-generated, don't edit it. Thanks.

namespace AlibabaCloud\Tea\Roa\Roa;

use AlibabaCloud\Tea\Model;

class Config extends Model
{
    public $accessKeyId;

    public $accessKeySecret;

    public $securityToken;

    public $protocol;

    public $regionId;

    public $readTimeout;

    public $connectTimeout;

    public $httpProxy;

    public $httpsProxy;

    public $credential;

    public $endpoint;

    public $noProxy;

    public $maxIdleConns;

    public $network;

    public $suffix;

    /**
     * @deprecated
     *
     * @var string
     */
    public $type;

    public function validate()
    {
    }

    public function toMap()
    {
        $res                    = [];
        $res['accessKeyId']     = $this->accessKeyId;
        $res['accessKeySecret'] = $this->accessKeySecret;
        $res['securityToken']   = $this->securityToken;
        $res['protocol']        = $this->protocol;
        $res['regionId']        = $this->regionId;
        $res['readTimeout']     = $this->readTimeout;
        $res['connectTimeout']  = $this->connectTimeout;
        $res['httpProxy']       = $this->httpProxy;
        $res['httpsProxy']      = $this->httpsProxy;
        $res['credential']      = null !== $this->credential ? $this->credential->toMap() : null;
        $res['endpoint']        = $this->endpoint;
        $res['noProxy']         = $this->noProxy;
        $res['maxIdleConns']    = $this->maxIdleConns;
        $res['network']         = $this->network;
        $res['suffix']          = $this->suffix;
        $res['type']            = $this->type;

        return $res;
    }

    /**
     * @param array $map
     *
     * @return Config
     */
    public static function fromMap($map = [])
    {
        $model = new self();
        if (isset($map['accessKeyId'])) {
            $model->accessKeyId = $map['accessKeyId'];
        }
        if (isset($map['accessKeySecret'])) {
            $model->accessKeySecret = $map['accessKeySecret'];
        }
        if (isset($map['securityToken'])) {
            $model->securityToken = $map['securityToken'];
        }
        if (isset($map['protocol'])) {
            $model->protocol = $map['protocol'];
        }
        if (isset($map['regionId'])) {
            $model->regionId = $map['regionId'];
        }
        if (isset($map['readTimeout'])) {
            $model->readTimeout = $map['readTimeout'];
        }
        if (isset($map['connectTimeout'])) {
            $model->connectTimeout = $map['connectTimeout'];
        }
        if (isset($map['httpProxy'])) {
            $model->httpProxy = $map['httpProxy'];
        }
        if (isset($map['httpsProxy'])) {
            $model->httpsProxy = $map['httpsProxy'];
        }
        if (isset($map['credential'])) {
            $model->credential = Credential::fromMap($map['credential']);
        }
        if (isset($map['endpoint'])) {
            $model->endpoint = $map['endpoint'];
        }
        if (isset($map['noProxy'])) {
            $model->noProxy = $map['noProxy'];
        }
        if (isset($map['maxIdleConns'])) {
            $model->maxIdleConns = $map['maxIdleConns'];
        }
        if (isset($map['network'])) {
            $model->network = $map['network'];
        }
        if (isset($map['suffix'])) {
            $model->suffix = $map['suffix'];
        }
        if (isset($map['type'])) {
            $model->type = $map['type'];
        }

        return $model;
    }
}
