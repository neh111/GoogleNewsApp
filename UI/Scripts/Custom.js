

//Get the neccasary data of the body topic and display it on the screen
function callServerSideMethod(title) {
    var parameter = title;
    $.ajax({
        type: "POST",
        url: "Main.aspx/ServerSideMethod",
        data: JSON.stringify({ parameter: parameter }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            var jsonObject = JSON.parse(response.d);
            var post = jsonObject[0];
            var postContainerDiv = document.getElementById("postContainer");
            // Clear existing content of the Div
            postContainerDiv.innerHTML = '';

            // Create h2 element
            var headTitle = document.createElement("h2");
            headTitle.textContent = "Post";
            postContainerDiv.appendChild(headTitle);

            // Create h3 element
            var title = document.createElement("h3");
            title.textContent = post.Title;
            postContainerDiv.appendChild(title);

            var wrapBodyDiv = document.createElement("div");
            wrapBodyDiv.id = "wrapDiv";
            postContainerDiv.appendChild(wrapBodyDiv);

            //Create a element
            var newsAElement = document.createElement('a');
            newsAElement.textContent = 'NEWS.com.au';
            newsAElement.target = "_blank";
            newsAElement.setAttribute('href', 'https://www.news.com.au/');
            newsAElement.className = "postBody";
            wrapBodyDiv.appendChild(newsAElement);
            

            // Create a element
            var titleAsLink = document.createElement("a");
            titleAsLink.href = post.Link;
            titleAsLink.target = "_blank";
            titleAsLink.innerText = post.Title;
            titleAsLink.className = "postBody";
            wrapBodyDiv.appendChild(titleAsLink);

            //Here should appear a paragraph of the content from the post.Link
            //loadTopicParagraph(post.Link);

            //Create ol elemnt
            var range = document.createRange();
            var fragment = range.createContextualFragment(post.Summary);
            fragment.className = "postBody";
            wrapBodyDiv.appendChild(fragment);

            //Create a element
            var allElement = document.createElement("a");
            allElement.href = "https://news.google.com/home?hl=en-IL&gl=IL&ceid=IL:en";
            allElement.target = "_blank";
            allElement.innerText = "all 2,636 news articles >>";
            allElement.className = "postBody";
            wrapBodyDiv.appendChild(allElement);

            //Create a element
            var readMoreElement = document.createElement("a");
            readMoreElement.href = post.Link;
            readMoreElement.target = "_blank";
            readMoreElement.innerText = "Read more >>";
            readMoreElement.style.color = "green";
            readMoreElement.style.float = "right";
            postContainerDiv.appendChild(readMoreElement);

            postContainerDiv.style.display = 'flex';
            postContainerDiv.style.flexDirection = 'column';
          
        },
        error: function (xhr, status, error) {
            console.error("Error calling server-side method: ", error);
        }
    });
}



function topicClicked(event) {
    // Prevent the default behavior of the link
    event.preventDefault();
    callServerSideMethod(event.target.innerText);
}



//The function try to get the document of the page of the post.Link
function loadTopicParagraph(url) {
    // Making an AJAX request
    $.ajax({
        url: url,
        method: "GET",
        success: function (data) {
            console.log('dataPage', data);
        },
        error: function (xhr, status, error) {
            console.log("Error fetching content:", error);
            console.log("Error fetching content:", xhr);
            console.log("Error fetching content:", status);
        }
    });
}

