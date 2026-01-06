document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".menu-button").forEach(btn => {
        btn.addEventListener("click", (e) => {
            e.stopPropagation();

            const id = btn.dataset.menu;
            const menu = document.getElementById(`menu-${id}`);

            document.querySelectorAll(".menu-dropdown").forEach(m => {
                if (m !== menu) m.style.display = "none";
            });

            menu.style.display = menu.style.display === "block" ? "none" : "block";
        });
    });

    document.addEventListener("click", () => {
        document.querySelectorAll(".menu-dropdown").forEach(m => m.style.display = "none");
    });
});