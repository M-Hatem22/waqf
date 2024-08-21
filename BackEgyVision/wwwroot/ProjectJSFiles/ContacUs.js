$(document).ready(function () {
    var loc = document.getElementById('location')
    var frame = document.createElement("iframe");
    frame.setAttribute("src", 'https://maps.google.com/maps?q=' + $('#Latitude').val() + ',' + $('#Longitude').val() + '&z=15&output=embed');
    frame.setAttribute("name", "Hello");
    frame.setAttribute("id", "locationFrame");
    frame.frameBorder = 0;
    frame.style.width = 100 + "%";
    frame.style.height = 250 + "px";
    loc.appendChild(frame);
});
