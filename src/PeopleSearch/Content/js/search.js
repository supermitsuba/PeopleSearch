$(function(){
    var page = 0;
    
    $('#searchPerson').keypress(function(event){
        var length = this.value.length
        var searchTerm = this.value + event.key
        var delaySeconds = document.getElementById('simulateDelay').checked ? 5 : 0;

        delay(function(){
            $('#loading').css('visibility', 'visible')
            if(length > 1) {
                $.ajax({
                    type: "GET",
                    url: "v1/api/people?prefix="+searchTerm+"&limit=10&offset=0&delay="+delaySeconds,
                    success: peopleSearch
                })
            }
        }, 2000)
    })

    function peopleSearch(data) {
        console.log(data)
            $('#loading').css('visibility', 'hidden')
    }

    var delay = (function(){
        var timer = 0
        return function(callback, ms){
            clearTimeout (timer)
            timer = setTimeout(callback, ms)
        }
    })()
})