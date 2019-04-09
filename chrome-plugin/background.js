var port = null;

chrome.runtime.onMessage.addListener(function(request, sender, callback){
	chrome.runtime.sendNativeMessage('com.my_company.my_application',
  	request,
  	function(response) {
			console.log("Received " + response);
			callback(response)
		});
	return true;
})