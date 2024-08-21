LoadProjectsCover();

$(document).ready(function () {
});

function LoadProjectsCover() {
    $.ajax({
        url: '/Home/LoadProject',
        type: 'POST',
        success: function (data) {
            //debugger;
            for (var i = 0; i < data.projects.length; i++) {
                debugger;
                if (i > 3) {
                    return;
                }
                var projectsDiv = document.createElement('div'); // is a node
                projectsDiv.classList.add("item");
                projectsDiv.classList.add("col-sm-4");
                projectsDiv.innerHTML = `  <a href="/Home/ProjectDetails?projectId=${data.projects[i].projectId}">
                                                <figure class="snip1584" data-toggle="modal" data-target="#myModal">
                                                    <img class="card-img" src="/Home/DownProjectCoverAttachment?projectId=${data.projects[i].projectId}" alt="">
                                                <figcaption>
                                                    <h3>${data.projects[i].projectTitleEn} </h3>
                                                    <h5>Project</h5>
                                                </figcaption>
                                                </figure>
                                            </a>`
                document.getElementById("ProjectsContainer").appendChild(projectsDiv);
            }
        },
        error: function (err) {

        },
        complete: function (data) {

        }
    });
}

function LoadProjectsCover1() {
    $.ajax({
        url: '/Home/LoadProjectCoverAttachments',
        type: 'POST',
        success: function (data) {
            for (var i = 0; i < data.projects.length; i++) {
                debugger;
                var img = new Image();
                img.style.width = "200px";
                img.style.height = "190px";
                img.style.border = "3px solid white !important";
                img.style.padding = "5px";
                img.classList.add("item");
                //img.style.borderRadius = "30%";
                img.src = "data:image/jpeg;base64," + data.projects[i].attachmentFile;

                document.querySelector('#ProjectsContainer').appendChild(img);
            }
        },
        error: function (err) {

        },
        complete: function (data) {

        }
    });
}