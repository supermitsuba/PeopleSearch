$(function(){
    var page = 0;
    var searchTerm = ''

    $('#searchPerson').keypress(function(event){
        var length = this.value.length
        searchTerm = this.value + event.key
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

    $('#nextPage').click(function(){
        page += 1
        var delaySeconds = document.getElementById('simulateDelay').checked ? 5 : 0;

        $.ajax({
            type: "GET",
            url: "v1/api/people?prefix="+searchTerm+"&limit=10&offset="+page+"&delay="+delaySeconds,
            success: peopleSearch
        })
    })

    $('#previousPage').click(function(){
        page -= 1
        var delaySeconds = document.getElementById('simulateDelay').checked ? 5 : 0;
        
        $.ajax({
            type: "GET",
            url: "v1/api/people?prefix="+searchTerm+"&limit=10&offset="+page+"&delay="+delaySeconds,
            success: peopleSearch
        })
    })

    function peopleSearch(data) {
        $('#loading').css('visibility', 'hidden')
        $('#results').css('visibility', 'visible')
        $('#resultsTable tbody tr').remove() // this clears previous rows

        // this is for adding each row
        for(var i = 0; i < data.length; i++) {
            var row = '<tr><td>'+ data[i].firstName
                +'</td><td>'+ data[i].lastName
                +'</td><td>'+ data[i].address1 + ' ' + data[i].city + ', ' + data[i].addressState + ' ' + data[i].zip
                +'</td><td>'+ data[i].age
                +'</td><td>'+ data[i].interests
                +'</td><td><img src="'+ data[i].pictureUrl 
                +'" style="width:50px;height:50px;" /></td></tr>'
            $('#resultsTable tbody').append(row);
        }

        //this is for displaying the next and previous page buttons
        if(data.length == 10) {
            $('#nextPage').css('visibility', 'visible')
        }
        else {
            $('#nextPage').css('visibility', 'hidden')
        }

        if(page > 0) {
            $('#previousPage').css('visibility', 'visible')
        }
        else {
            $('#previousPage').css('visibility', 'hidden')
        }
    }

    var delay = (function(){
        var timer = 0
        return function(callback, ms){
            clearTimeout (timer)
            timer = setTimeout(callback, ms)
        }
    })()
})