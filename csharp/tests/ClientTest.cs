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
                //提供一个简单的、可通过编程方式控制的 HTTP 协议侦听器。此类不能被继承。
                httpobj = new HttpListener();
                //定义url及端口号，通常设置为配置文件
                httpobj.Prefixes.Add("http://127.0.0.1:1500/");
                //启动监听器
                httpobj.Start();
                //异步监听客户端请求，当客户端的网络请求到来时会自动执行Result委托
                //该委托没有返回值，有一个IAsyncResult接口的参数，可通过该参数获取context对象
                httpobj.BeginGetContext(Result, null);
                Console.WriteLine($"server init, waiting for request,time：{DateTime.Now.ToString()}\r\n");
            }
        }

        private static void Result(IAsyncResult ar)
        {
            //当接收到请求后程序流会走到这里

            //继续异步监听
            httpobj.BeginGetContext(Result, null);
            var guid = Guid.NewGuid().ToString();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"new request guid:{guid},time：{DateTime.Now.ToString()}");
            //获得context对象
            var context = httpobj.EndGetContext(ar);
            var request = context.Request;
            var response = context.Response;
            context.Response.ContentType = "application/json";//告诉客户端返回的ContentType类型为纯文本格式，编码为UTF-8
            context.Response.ContentEncoding = Encoding.UTF8;
            string returnObj = null;//定义返回客户端的信息
            returnObj = HandleRequest(request, response);
            var returnByteArr = Encoding.UTF8.GetBytes(returnObj);//设置客户端返回信息的编码
            try
            {
                using (var stream = response.OutputStream)
                {
                    //把处理信息返回到客户端
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"connect error：{ex.ToString()}");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"request finish guid：{guid},time：{ DateTime.Now.ToString()}\r\n");
        }

        private static string HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string data = null;
            string action = request.QueryString.Get("action");

            switch(action)
            {
                case "GetSuccess":
                    {
                        response.StatusDescription = "200";//获取或设置返回给客户端的 HTTP 状态代码的文本说明。
                        response.StatusCode = 200;// 获取或设置返回给客户端的 HTTP 状态代码。
                        return "{\"result\": \"server test\"}";
                    }
                case "PostError":
                    {
                        response.StatusDescription = "400";//获取或设置返回给客户端的 HTTP 状态代码的文本说明。
                        response.StatusCode = 400;// 获取或设置返回给客户端的 HTTP 状态代码。
                        return "{\"result\": \"server server error\"}";
                    }
            }
            //try
            //{
            //    var byteList = new List<byte>();
            //    var byteArr = new byte[2048];
            //    int readLen = 0;
            //    int len = 0;
            //    //接收客户端传过来的数据并转成字符串类型
            //    do
            //    {
            //        readLen = request.InputStream.Read(byteArr, 0, byteArr.Length);
            //        len += readLen;
            //        byteList.AddRange(byteArr);
            //    } while (readLen != 0);
            //    data = Encoding.UTF8.GetString(byteList.ToArray(), 0, len);

            //    //获取得到数据data可以进行其他操作
            //}
            //catch (Exception ex)
            //{
            //    response.StatusDescription = "404";
            //    response.StatusCode = 404;
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine($"在接收数据时发生错误:{ex.ToString()}");
            //    return $"在接收数据时发生错误:{ex.ToString()}";
            //}
            response.StatusDescription = "200";//获取或设置返回给客户端的 HTTP 状态代码的文本说明。
            response.StatusCode = 200;// 获取或设置返回给客户端的 HTTP 状态代码。
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"request finish:{data?.Trim()},time：{DateTime.Now.ToString()}");
            return $"接收数据完成";
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
