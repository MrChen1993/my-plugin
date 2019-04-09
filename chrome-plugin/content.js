var launch_message;
document.addEventListener('myCustomEvent', function(evt) {
  const detail = JSON.parse(evt.detail);
  const {id, ...other} = detail
  chrome.runtime.sendMessage(other, function(response){
    responseToDom(id, response)
  })
}, false);

function responseToDom(id, response){
  var evt = document.createEvent("CustomEvent")
  evt.initCustomEvent("myCustomEventResponse", true, false, JSON.stringify({id, response}));
  document.dispatchEvent(evt);
}