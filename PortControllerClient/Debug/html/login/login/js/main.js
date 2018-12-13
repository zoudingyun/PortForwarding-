window.onload=function(){
    setUserName();
   }
   
   
function getQueryString(name) { 
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i"); 
        var r = window.location.search.substr(1).match(reg); 
        if (r != null) return unescape(r[2]); 
        return null; 
    } 
	
function setUserName(){
	document.getElementById('message').innerHTML='■ 欢迎你：'+getQueryString('userName')+'&nbsp&nbsp&nbsp&nbsp'+' ■ 组：'+getQueryString('inherit');
}