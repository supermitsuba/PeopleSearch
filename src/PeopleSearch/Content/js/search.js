$(function(){
    $('#searchPerson').keypress(function(event){
        var length = this.value.length

        delay(function(){
            if(length > 1) {
                console.log(length)
            }
        }, 2000)
    })

    var delay = (function(){
        var timer = 0;
        return function(callback, ms){
            clearTimeout (timer);
            timer = setTimeout(callback, ms);
        };
    })();
})