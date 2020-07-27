using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AlibabaCloud.ROAClient;
using AlibabaCloud.ROAClient.Models;
using AlibabaCloud.TeaUtil.Models;
using Xunit;

namespace test
{
    public class ClientTest
    {
        static HttpListener httpobj;

        public ClientTest()
        {
            if(httpobj == null)
            {
                //�ṩһ���򵥵ġ���ͨ����̷�ʽ���Ƶ� HTTP Э�������������಻�ܱ��̳С�
                httpobj = new HttpListener();
                //����url���˿ںţ�ͨ������Ϊ�����ļ�
                httpobj.Prefixes.Add("http://127.0.0.1:1500/");
                //����������
                httpobj.Start();
                //�첽�����ͻ������󣬵��ͻ��˵�����������ʱ���Զ�ִ��Resultί��
                //��ί��û�з���ֵ����һ��IAsyncResult�ӿڵĲ�������ͨ���ò�����ȡcontext����
                httpobj.BeginGetContext(Result, null);
                Console.WriteLine($"server init, waiting for request,time��{DateTime.Now.ToString()}\r\n");
            }
        }

        private static void Result(IAsyncResult ar)
        {
            //�����յ��������������ߵ�����

            //�����첽����
            httpobj.BeginGetContext(Result, null);
            var guid = Guid.NewGuid().ToString();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"new request guid:{guid},time��{DateTime.Now.ToString()}");
            //���context����
            var context = httpobj.EndGetContext(ar);
            var request = context.Request;
            var response = context.Response;
            context.Response.ContentType = "application/json";//���߿ͻ��˷��ص�ContentType����Ϊ���ı���ʽ������ΪUTF-8
            context.Response.ContentEncoding = Encoding.UTF8;
            string returnObj = null;//���巵�ؿͻ��˵���Ϣ
            returnObj = HandleRequest(request, response);
            var returnByteArr = Encoding.UTF8.GetBytes(returnObj);//���ÿͻ��˷�����Ϣ�ı���
            try
            {
                using (var stream = response.OutputStream)
                {
                    //�Ѵ�����Ϣ���ص��ͻ���
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"connect error��{ex.ToString()}");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"request finish guid��{guid},time��{ DateTime.Now.ToString()}\r\n");
        }

        private static string HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string data = null;
            string action = request.QueryString.Get("action");

            switch(action)
            {
                case "GetSuccess":
                    {
                        response.StatusDescription = "200";//��ȡ�����÷��ظ��ͻ��˵� HTTP ״̬������ı�˵����
                        response.StatusCode = 200;// ��ȡ�����÷��ظ��ͻ��˵� HTTP ״̬���롣
                        return "{\"result\": \"server test\"}";
                    }
                case "PostError":
                    {
                        response.StatusDescription = "400";//��ȡ�����÷��ظ��ͻ��˵� HTTP ״̬������ı�˵����
                        response.StatusCode = 400;// ��ȡ�����÷��ظ��ͻ��˵� HTTP ״̬���롣
                        return "{\"result\": \"server server error\"}";
                    }
            }
            //try
            //{
            //    var byteList = new List<byte>();
            //    var byteArr = new byte[2048];
            //    int readLen = 0;
            //    int len = 0;
            //    //���տͻ��˴����������ݲ�ת���ַ�������
            //    do
            //    {
            //        readLen = request.InputStream.Read(byteArr, 0, byteArr.Length);
            //        len += readLen;
            //        byteList.AddRange(byteArr);
            //    } while (readLen != 0);
            //    data = Encoding.UTF8.GetString(byteList.ToArray(), 0, len);

            //    //��ȡ�õ�����data���Խ�����������
            //}
            //catch (Exception ex)
            //{
            //    response.StatusDescription = "404";
            //    response.StatusCode = 404;
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine($"�ڽ�������ʱ��������:{ex.ToString()}");
            //    return $"�ڽ�������ʱ��������:{ex.ToString()}";
            //}
            response.StatusDescription = "200";//��ȡ�����÷��ظ��ͻ��˵� HTTP ״̬������ı�˵����
            response.StatusCode = 200;// ��ȡ�����÷��ظ��ͻ��˵� HTTP ״̬���롣
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"request finish:{data?.Trim()},time��{DateTime.Now.ToString()}");
            return $"�����������";
        }


        [Fact]
        public void Test_DoRequest()
        {
            var config = new Config
            {
                AccessKeyId = "access_key_id",
                AccessKeySecret = "access_key_secret",
                SecurityToken = "security_token",
                Protocol = "http",
                RegionId = "region_id",
                ReadTimeout = 1000,
                ConnectTimeout = 5000,
                Endpoint = "127.0.0.1:1500",
                MaxIdleConns = 1,
            };
            var runtime = new RuntimeOptions
            {
                Autoretry = false,
                MaxAttempts = 2
            };

            var client = new Client(config);
            var query = new Dictionary<string, string>
            {
                { "action", "GetSuccess" }
            };
            var res = client.DoRequest("version", "http", "GET", "auth_type", "", query, new Dictionary<string, string>(), null, runtime);
            Assert.Equal("server test", ((Dictionary<string, object>)res["body"])["result"].ToString());

            var errQuery = new Dictionary<string, string>
            {
                { "action", "PostError" }
            };
            var exception =  Assert.Throws<Tea.TeaUnretryableException>(() =>
            {
                client.DoRequest("version", "http", "POST", "auth_type", "", errQuery, new Dictionary<string, string>(), null, runtime);
            });
            Assert.Contains("code: 400", exception.Message);

        }

        [Fact]
        public async Task Test_DoRequestAsync()
        {
            var config = new Config
            {
                AccessKeyId = "access_key_id",
                AccessKeySecret = "access_key_secret",
                SecurityToken = "security_token",
                Protocol = "http",
                RegionId = "region_id",
                ReadTimeout = 1000,
                ConnectTimeout = 5000,
                Endpoint = "127.0.0.1:1500",
                MaxIdleConns = 1,
            };
            var runtime = new RuntimeOptions
            {
                Autoretry = false,
                MaxAttempts = 2
            };

            var client = new Client(config);
            var query = new Dictionary<string, string>
            {
                { "action", "GetSuccess" }
            };

            var res = await client.DoRequestAsync("version", "http", "GET", "auth_type", "", query, new Dictionary<string, string>(), null, runtime);
            Assert.Equal("server test", ((Dictionary<string, object>)res["body"])["result"].ToString());

            var errQuery = new Dictionary<string, string>
            {
                { "action", "PostError" }
            };
            var exception = await Assert.ThrowsAsync<Tea.TeaUnretryableException>(async () =>
            {
                await client.DoRequestAsync("version", "http", "POST", "auth_type", "", errQuery, new Dictionary<string, string>(), null, runtime);
            });
            Assert.Contains("code: 400", exception.Message);
        }

        [Fact]
        public void Test_DoRequestWithAction()
        {
            var config = new Config
            {
                AccessKeyId = "access_key_id",
                AccessKeySecret = "access_key_secret",
                SecurityToken = "security_token",
                Protocol = "http",
                RegionId = "region_id",
                ReadTimeout = 1000,
                ConnectTimeout = 5000,
                Endpoint = "127.0.0.1:1500",
                MaxIdleConns = 1,
            };
            var runtime = new RuntimeOptions
            {
                Autoretry = false,
                MaxAttempts = 2
            };

            var client = new Client(config);
            var query = new Dictionary<string, string>
            {
                { "action", "GetSuccess" }
            };
            var res = client.DoRequestWithAction("getMessage", "version", "http", "GET", "auth_type", "", query, new Dictionary<string, string>(), null, runtime);
            Assert.Equal("server test", ((Dictionary<string, object>)res["body"])["result"].ToString());

            var errQuery = new Dictionary<string, string>
            {
                { "action", "PostError" }
            };
            var exception = Assert.Throws<Tea.TeaUnretryableException>(() =>
            {
                client.DoRequestWithAction("getMessage", "version", "http", "POST", "auth_type", "", errQuery, new Dictionary<string, string>(), null, runtime);
            });
            Assert.Contains("code: 400", exception.Message);
        }

        [Fact]
        public async Task Test_DoRequestWithActionAsync()
        {
            var config = new Config
            {
                AccessKeyId = "access_key_id",
                AccessKeySecret = "access_key_secret",
                SecurityToken = "security_token",
                Protocol = "http",
                RegionId = "region_id",
                ReadTimeout = 1000,
                ConnectTimeout = 5000,
                Endpoint = "127.0.0.1:1500",
                MaxIdleConns = 1,
            };
            var runtime = new RuntimeOptions
            {
                Autoretry = false,
                MaxAttempts = 2
            };

            var client = new Client(config);
            var query = new Dictionary<string, string>
            {
                { "action", "GetSuccess" }
            };

            var res = await client.DoRequestWithActionAsync("getMessage", "version", "http", "GET", "auth_type", "", query, new Dictionary<string, string>(), null, runtime);
            Assert.Equal("server test", ((Dictionary<string, object>)res["body"])["result"].ToString());

            var errQuery = new Dictionary<string, string>
            {
                { "action", "PostError" }
            };
            var exception = await Assert.ThrowsAsync<Tea.TeaUnretryableException>(async () =>
            {
                await client.DoRequestWithActionAsync("getMessage", "version", "http", "POST", "auth_type", "", errQuery, new Dictionary<string, string>(), null, runtime);
            });
            Assert.Contains("code: 400", exception.Message);
        }

        [Fact]
        public void Test_DoRequestWithForm()
        {
            var config = new Config
            {
                AccessKeyId = "access_key_id",
                AccessKeySecret = "access_key_secret",
                SecurityToken = "security_token",
                Protocol = "http",
                RegionId = "region_id",
                ReadTimeout = 1000,
                ConnectTimeout = 5000,
                Endpoint = "127.0.0.1:1500",
                MaxIdleConns = 1,
            };
            var runtime = new RuntimeOptions
            {
                Autoretry = false,
                MaxAttempts = 2
            };

            var client = new Client(config);
            var query = new Dictionary<string, string>
            {
                { "action", "GetSuccess" }
            };
            var res = client.DoRequestWithForm("version", "http", "GET", "auth_type", "", query, new Dictionary<string, string>(), null, runtime);
            Assert.Equal("server test", ((Dictionary<string, object>)res["body"])["result"].ToString());

            var errQuery = new Dictionary<string, string>
            {
                { "action", "PostError" }
            };
            var exception = Assert.Throws<Tea.TeaUnretryableException>(() =>
            {
                client.DoRequestWithForm("version", "http", "POST", "auth_type", "", errQuery, new Dictionary<string, string>(), null, runtime);
            });
            Assert.Contains("code: 400", exception.Message);
        }

        [Fact]
        public async Task Test_DoRequestWithToFormAsync()
        {
            var config = new Config
            {
                AccessKeyId = "access_key_id",
                AccessKeySecret = "access_key_secret",
                SecurityToken = "security_token",
                Protocol = "http",
                RegionId = "region_id",
                ReadTimeout = 1000,
                ConnectTimeout = 5000,
                Endpoint = "127.0.0.1:1500",
                MaxIdleConns = 1,
            };
            var runtime = new RuntimeOptions
            {
                Autoretry = false,
                MaxAttempts = 2
            };

            var client = new Client(config);
            var query = new Dictionary<string, string>
            {
                { "action", "GetSuccess" }
            };

            var res = await client.DoRequestWithFormAsync("version", "http", "GET", "auth_type", "", query, new Dictionary<string, string>(), null, runtime);
            Assert.Equal("server test", ((Dictionary<string, object>)res["body"])["result"].ToString());

            var errQuery = new Dictionary<string, string>
            {
                { "action", "PostError" }
            };
            var exception = await Assert.ThrowsAsync<Tea.TeaUnretryableException>(async () =>
            {
                await client.DoRequestWithFormAsync("version", "http", "POST", "auth_type", "", errQuery, new Dictionary<string, string>(), null, runtime);
            });
            Assert.Contains("code: 400", exception.Message);
        }
    }
}
