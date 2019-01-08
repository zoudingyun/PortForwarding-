var tableData;
var dataRowNum = 0;

window.onload=function(){
    setUserName();
	getUserConf();
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

function getUserConf(){
	var params={'userId':''};
	params.userId = getQueryString('usr');
	
	getPortInfo(params)
}


function savePortInfo(){
	var conf = new Array();
	var index = new Array();
	var a = getData();
	for(var i=0;i<a.length;i++){
		index.push(a[i].id);
	}
	for(var i=0;i<index.length;i++){
		conf.push(getPortCondition(index[i]));
	}
	
	postPortInfo(conf);
}

function postPortInfo(config){
	$.ajax({
				url: AjaxUrl + "/user/saveConf"
				, dataType: 'JSON'
				, async: false//先确认权限后查询单位，所以用同步
				, beforeSend: function() {
					$(".loadingGif").show();
				}
				, complete: function() {
					$(".loadingGif").hide();
				}
				, data: JSON.stringify(config)
				, contentType: "application/json"
				, crossDomain: true
				, type: "POST"
				, success:function(response){
					
					ret = response;
					//if(ret.)
					if(ret.message == 'SUCCESS'){
						//alert(ret.message+':'+ret.data.state)
						
						try{
							var windowRet = window.external.startPorts(JSON.stringify(config));
							alert(windowRet)
							}catch(err){
								
							}
						
					}else{
						//ret.data.userName
						//saveUserMessage(ret.data.userName,ret.data.inherit);
					}

					
			}
	});
	//$(".loadingGif").hide();
}

function getPortInfo(user){
	$.ajax({
				url: AjaxUrl + "/user/getConf"
				, dataType: 'JSON'
				, async: false//先确认权限后查询单位，所以用同步
				, beforeSend: function() {
					$(".loadingGif").show();
				}
				, complete: function() {
					$(".loadingGif").hide();
				}
				, data: JSON.stringify(user)
				, contentType: "application/json"
				, crossDomain: true
				, type: "POST"
				, success:function(response){
					
					ret = response;
					//if(ret.)
					if(ret.message == 'SUCCESS'){
						//alert(ret.message+':'+ret.data.state)
						tableData = ret.data;
						for(var i=0;i<tableData.length;i++){
						var _data = {
									"id":i,
									"action" : "<input class='btn btn-danger' style='margin: 5px;' type='submit' onclick='deleteConf("+i+");' value='×' >",
									"forwardPassNet" : getSelect(tableData[i].forwardPassNet,i) ,
									"agentAdd":"<input id='agentAdd-"+i+"' class='form-control' value='"+tableData[i].agentAdd+"'>",
									"agentPort":"<input id='agentPort-"+i+"' class='form-control' value='"+tableData[i].agentPort+"'>",
									"targetAdd":"<input id='targetAdd-"+i+"' class='form-control' value='"+tableData[i].targetAdd+"'>",
									"targetPort":"<input id='targetPort-"+i+"' class='form-control' value='"+tableData[i].targetPort+"'>",
									"targetPwd":"<input id='targetPwd-"+i+"' class='form-control' type='password' value='"+tableData[i].targetPwd+"'>"
									}
							
						$("#list").bootstrapTable('append', _data);
						dataRowNum = i;
						
						/* $("#list").append("<tr><td>"
						+"<input class='btn btn-danger' style='margin: 5px;' type='submit' onclick='login();' value='×' ></td><td>"
						  +"<select id='forwardPassNet-0' style='color: #000000;'>"
							+"<option>转发直连</option>"
							+"<option>内网穿透</option>"
						  +"</select>	  </td>"
							  +"<td><input id='agentAdd-0' class='form-control' value='"+tableData[i].agentAdd+"'></td>"
							  +"<td><input id='agentPort-0' class='form-control' value='"+tableData[i].agentAdd+"'></td>"
							  +"<td><input id='targetAdd-0' class='form-control' value='"+tableData[i].agentAdd+"'></td>"
							  +"<td><input id='targetPort-0' class='form-control' value='"+tableData[i].agentAdd+"'></td></tr>"); */
							  }
					}else{
						//ret.data.userName
						//saveUserMessage(ret.data.userName,ret.data.inherit);
					}

					
			}
	});
	//$(".loadingGif").hide();
}

function getPortCondition(index) {
        var params={'forwardPassNet':'','agentAdd':'','agentPort':'','targetAdd':'','targetPort':'','userId':'','targetPwd':''};
        params.forwardPassNet=document.getElementById('forwardPassNet-'+index).value;
        params.agentAdd=document.getElementById('agentAdd-'+index).value;
		params.agentPort=document.getElementById('agentPort-'+index).value;
		params.targetAdd=document.getElementById('targetAdd-'+index).value;
		params.targetPort=document.getElementById('targetPort-'+index).value;
		params.targetPwd=document.getElementById('targetPwd-'+index).value;
		params.userId='zdy';
        return params;
    }
	
function deleteConf(index){
	var ids = [];
	ids.push(index);
	$("#list").bootstrapTable('remove', {field: 'id', values: ids});
}

function addConf(){
	dataRowNum++;
	var _data = {
				"id":dataRowNum,
				"action" : "<input class='btn btn-danger' style='margin: 5px;' type='submit' onclick='deleteConf("+dataRowNum+");' value='×' >",
				"forwardPassNet" : "<select id='forwardPassNet-"+dataRowNum+"' style='color: #000000;'>"
							+"<option>转发直连</option>"
							+"<option>内网穿透</option>"
						+"</select>"    ,
				"agentAdd":"<input id='agentAdd-"+dataRowNum+"' class='form-control'>",
				"agentPort":"<input id='agentPort-"+dataRowNum+"' class='form-control'>",
				"targetAdd":"<input id='targetAdd-"+dataRowNum+"' class='form-control'>",
				"targetPort":"<input id='targetPort-"+dataRowNum+"' class='form-control'>",
				"targetPwd":"<input id='targetPwd-"+dataRowNum+"' class='form-control'>"
				}
		
	$("#list").bootstrapTable('append', _data);
}

function getData(){
	return $("#list").bootstrapTable('getData');
}

function getSelect(choose,i){
	if(choose=="内网穿透"){
		return "<select id='forwardPassNet-"+i+"' style='color: #000000;'>"
												+"<option>转发直连</option>"
												+"<option selected = 'selected' >内网穿透</option>"
											  +"</select>";
	}else{
		return "<select id='forwardPassNet-"+i+"' style='color: #000000;'>"
												+"<option  selected = 'selected' >转发直连</option>"
												+"<option>内网穿透</option>"
											+"</select>";
	}
}