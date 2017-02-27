$(function(){
    var page = 0;
    var searchTerm = ''

    /*
    The purpose of this function is to intercept which key is pressed.

    If a key is pressed, this function will want 2 seconds before executing the API call.
    */
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

    /*
    The purpose of this function is to go to the next page of search results.
    */
    $('#nextPage').click(function(){
        page += 1
        var delaySeconds = document.getElementById('simulateDelay').checked ? 5 : 0;

        $.ajax({
            type: "GET",
            url: "v1/api/people?prefix="+searchTerm+"&limit=10&offset="+page+"&delay="+delaySeconds,
            success: peopleSearch
        })
    })

    /*
    The purpose of this function is to go to the previous page of search results.
    */
    $('#previousPage').click(function(){
        page -= 1
        var delaySeconds = document.getElementById('simulateDelay').checked ? 5 : 0;
        
        $.ajax({
            type: "GET",
            url: "v1/api/people?prefix="+searchTerm+"&limit=10&offset="+page+"&delay="+delaySeconds,
            success: peopleSearch
        })
    })

    /*
    The purpose of this function is to take the results of the search API call and populate a table.

    This will also show and hide elements.
    */
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

    /*
    The purpose of this function is to delay the execution of a function, until the user has stopped calling it.
    */
    var delay = (function(){
        var timer = 0
        return function(callback, ms){
            clearTimeout (timer)
            timer = setTimeout(callback, ms)
        }
    })()
})