// Configuration
let innerImageUrl = document.currentScript.getAttribute("innerImageUrl");
let wrapperImageUrl = document.currentScript.getAttribute("wrapperImageUrl");
let mainText = document.getElementById("MainText").value;
let extraText = document.getElementById("ExtraText").value;
let canvas = document.getElementById('memeResultCanvas');
let ctx = canvas.getContext('2d');
let wrapperImage = new Image();
wrapperImage.onload = drawWrapperImage;
wrapperImage.src = wrapperImageUrl;
let innerImage = new Image();
innerImage.onload = drawInnerImage;
innerImage.src = innerImageUrl;
function drawWrapperImage() {
    canvas.width = this.naturalWidth;
    canvas.height = this.naturalHeight;
    ctx.drawImage(this, 0, 0);
    ctx.fillStyle = "white";
    ctx.font = "37px serif";
    ctx.textAlign = "center";
    ctx.fillText(mainText, canvas.width / 2, 435);
    ctx.font = "26px serif";
    ctx.textAlign = "center";
    ctx.fillText(extraText, canvas.width / 2, 475);
}
function drawInnerImage() {
    ctx.drawImage(this, 62, 40, 516, 346);
    const img = new Image();
    img.src = canvas.toDataURL("image/png");
    img.onload = function () {
        uploadMemeToDb(innerImageUrl.replace(/^.*[\\]/, ""), // gets only file name with extension
        img);
    };
    img.classList.add("img-fluid");
    document.querySelector(".meme_result_image").appendChild(img);
}
document.getElementById("loadingIcon").remove();
function uploadMemeToDb(imgName, img) {
    let formData = new FormData();
    formData.append("imageName", imgName);
    formData.append("image", img.src);
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/memes/create");
    xhr.setRequestHeader("Accept", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            console.log("Data has been sent");
            console.log("Request status: ", xhr.status);
        }
    };
    xhr.send(formData);
}
//# sourceMappingURL=createMemeScript.js.map