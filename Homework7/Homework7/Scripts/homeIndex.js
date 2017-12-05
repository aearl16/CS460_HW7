/***************************************************************************
* This method will call the C# controller method based on the search click.*
* It will add the user's input from the text box and put it into the query *
* string to pass to the controller. It will get the returned data and      *
* call the displayStuff method to append images to the display area        *
***************************************************************************/
$("#searchButton").click(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Giphy/Search?find=" + $("#searchBox").val() +"&rating=" + $("#rating").val(),
        success: function (data) {
            displayStuff(data);
        },
        error: function (data) {
            alert("There was an error. Try again please!");
        }
    });

});

/*//////////////////////////////////////////////////////////////////////////
* Empties the display area and appends the giphy return data to the        *
* display area. Then calls the set size method to fix the size based on    *
* ths user's input into the drop down menus                                *
* input: data | the data from the Giphy Json                               *
*///////////////////////////////////////////////////////////////////////////
function displayStuff(data) {
    $("#displayArea").empty();
    $.each(data, function (i, item) {
        $("#displayArea").append("<div class='giphy-container'><img class='img-responsive' src='" + item["image"] + "' /></div>")
    });
    setSize();
}

//If size changes change the set size of the images
$("#size").change(setSize); 

/*
* Sets image size actively using JQuery
*/
function setSize() {
    $(".giphy-container").removeClass("container-sm");
    $(".giphy-container").removeClass("container-md");
    $(".giphy-container").removeClass("container-lg");
    /***********************************************************************
     * add a class to the container based on the #size select element      *
     *      #size value |   string appended                                *
     *      "sm"        |   container-sm (default)                         *
     *      "md"        |   container-md                                   *
     *      "lg"        |   container-lg (final)                           *
     ***********************************************************************/
    $(".giphy-container").addClass(($("#size").val() == "sm")
                    ? "container-sm"
                    : (($("#size").val() == "md")
                    ? "container-md" : "container-lg"));
}