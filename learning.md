ASP.net 
- Web Form
- MVC
- ...
## asp.net
是微软公司的.Net技术框架下的B/S(网页方向)框架技术.
## ado.net
 则是由asp.net编程语言编写的数据访问层的总括. 

- SQL
- ORM  ： LINQ

##技术

- LINQ
- MVC
- route

## Learning

### 热键：
- debug: F5
- run: Ctrl + F5
- complie: Ctrl + Shift + B 
-  实时更新：run+compile +refresh
- ctrl + alt + p :运行->调试
- F12 审查元素 浏览器
HttpUtility.encodeHttp()防止代码注入

### Controller
return View()返回View下与action同名的view

### Route 
默认路由为 Controller/Action/Id（url参数）

### View
- Razor引擎 
- 是一个标记：在服务器端执行后再向browser返回 因此可访问数据库
能在cshtml文件中插入C#语句，调用服务器端语句
@{ total = 7;}
<!-- inline --!>
<p> total value = @total</p>
注释：@* *@ /* */

将自定义的cshtml注入到Render中
