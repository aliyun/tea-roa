# -*- coding: utf-8 -*-
# This file is auto-generated, don't edit it. Thanks.
from Tea.model import TeaModel


class Config(TeaModel):
    """
    Model for initing client
    """
    def __init__(self, access_key_id=None, access_key_secret=None, security_token=None, protocol=None,
                 region_id=None, read_timeout=None, connect_timeout=None, http_proxy=None, https_proxy=None, credential=None,
                 endpoint=None, no_proxy=None, user_agent=None, max_idle_conns=None, network=None, suffix=None, type=None):
        # accesskey id
        self.access_key_id = access_key_id
        # accesskey secret
        self.access_key_secret = access_key_secret
        # security token
        self.security_token = security_token
        # http protocol
        self.protocol = protocol
        # region id
        self.region_id = region_id
        # read timeout
        self.read_timeout = read_timeout
        # connect timeout
        self.connect_timeout = connect_timeout
        # http proxy
        self.http_proxy = http_proxy
        # https proxy
        self.https_proxy = https_proxy
        # credential
        self.credential = credential
        # endpoint
        self.endpoint = endpoint
        # proxy white list
        self.no_proxy = no_proxy
        # user agent
        self.user_agent = user_agent
        # max idle conns
        self.max_idle_conns = max_idle_conns
        # network for endpoint
        self.network = network
        # suffix for endpoint
        self.suffix = suffix
        # credential type
        self.type = type

    def validate(self):
        if self.region_id:
            self.validate_pattern(self.region_id, 'region_id', '^[a-zA-Z0-9_-]+$')
        if self.network:
            self.validate_pattern(self.network, 'network', '^[a-zA-Z0-9_-]+$')
        if self.suffix:
            self.validate_pattern(self.suffix, 'suffix', '^[a-zA-Z0-9_-]+$')

    def to_map(self):
        result = {}
        result['accessKeyId'] = self.access_key_id
        result['accessKeySecret'] = self.access_key_secret
        result['securityToken'] = self.security_token
        result['protocol'] = self.protocol
        result['regionId'] = self.region_id
        result['readTimeout'] = self.read_timeout
        result['connectTimeout'] = self.connect_timeout
        result['httpProxy'] = self.http_proxy
        result['httpsProxy'] = self.https_proxy
        result['credential'] = self.credential
        result['endpoint'] = self.endpoint
        result['noProxy'] = self.no_proxy
        result['userAgent'] = self.user_agent
        result['maxIdleConns'] = self.max_idle_conns
        result['network'] = self.network
        result['suffix'] = self.suffix
        result['type'] = self.type
        return result

    def from_map(self, map={}):
        self.access_key_id = map.get('accessKeyId')
        self.access_key_secret = map.get('accessKeySecret')
        self.security_token = map.get('securityToken')
        self.protocol = map.get('protocol')
        self.region_id = map.get('regionId')
        self.read_timeout = map.get('readTimeout')
        self.connect_timeout = map.get('connectTimeout')
        self.http_proxy = map.get('httpProxy')
        self.https_proxy = map.get('httpsProxy')
        self.credential = map.get('credential')
        self.endpoint = map.get('endpoint')
        self.no_proxy = map.get('noProxy')
        self.user_agent = map.get('userAgent')
        self.max_idle_conns = map.get('maxIdleConns')
        self.network = map.get('network')
        self.suffix = map.get('suffix')
        self.type = map.get('type')
        return self
