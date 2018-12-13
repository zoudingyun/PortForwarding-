
var ret;
function saveUserMessage(userName,inherit){
		window.external.saveUserMessage(userName,inherit);
	}


function login(){
	//document.getElementById('load').style.display="block";
	//document.getElementById('username')
	$.ajax({
				url: AjaxUrl + "/login/usrAndPwd"
				, dataType: 'JSON'
				, async: false//先确认权限后查询单位，所以用同步
				, beforeSend: function() {
					$(".loadingGif").show();
				}
				, complete: function() {
					$(".loadingGif").hide();
				}
				, data: JSON.stringify(getQueryCondition())
				, contentType: "application/json"
				, crossDomain: true
				, type: "POST"
				, success:function(response){
					
					ret = response;
					//if(ret.)
					if(ret.message != 'SUCCESS'){
						//alert(ret.message+':'+ret.data.state)
						Helper.ui.dialog({
							title: ret.message,
							content: ret.data.state,
							darkMode: true
						});
					}else{
						//ret.data.userName
						saveUserMessage(ret.data.userName,ret.data.inherit);
					}

					
			}
	});
	//$(".loadingGif").hide();
}

function getQueryCondition() {
        var params={'usr':'','pwd':''};
        params.usr=document.getElementById('username').value;
        params.pwd=document.getElementById('password').value;
        return params;
    }
	