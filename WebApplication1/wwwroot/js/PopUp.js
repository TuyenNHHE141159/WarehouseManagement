const popup = document.querySelector(".popup");
const yesBtn = document.querySelector(".yes-btn");
const noBtn = document.querySelector(".no-btn");

function showPopup() {
    popup.style.display = "block";
}

function hidePopup() {
    popup.style.display = "none";
}

yesBtn.addEventListener("click", () => {
    console.log("Yes button clicked");
    hidePopup();
});

noBtn.addEventListener("click", () => {
    console.log("No button clicked");
    hidePopup();
});
