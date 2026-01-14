document.addEventListener("DOMContentLoaded", event => {
    const modal = document.getElementById("modal");
    const modalMessage = document.getElementById("modal-message");
    const yesButton = document.getElementById("modal-yes");
    const noButton = document.getElementById("modal-no");

    let deleteUrl = null;

    document.querySelectorAll(".delete-link").forEach(link => {
        link.addEventListener("click", e => {
            e.preventDefault();

            const name = link.dataset.name;
            deleteUrl = link.dataset.url;
            modalMessage.innerText = `${name}を削除します。この操作は元に戻せませんが、よろしいですか？`;
            modal.style.display = "flex";

            // モーダルを開いている時はスクロール不可とする
            document.body.style.overflow = "hidden";
        })
    });

    yesButton.addEventListener("click", e => {
        if (deleteUrl) {
            window.location.href = deleteUrl;
        }
    });

    noButton.addEventListener("click", e => {
        modal.style.display = "none";

        document.body.style.overflow = "auto";
    });

    // ESCキーで閉じられるように設定しておく
    document.addEventListener("keydown", event => {
        if (event.key === "Escape" && modal.style.display === "flex") {
            modal.style.display = "none";
            document.body.style.overflow = "auto";
        }
    });
});