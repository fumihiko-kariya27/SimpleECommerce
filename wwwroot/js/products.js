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

// 商品購入ボタン押下時の設定
document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("purchase-modal");
    const nameEl = document.getElementById("purchase-name");
    const priceEl = document.getElementById("purchase-price");
    const stockEl = document.getElementById("purchase-stock");
    const quantityEl = document.getElementById("purchase-quantity");

    let currentStock = 0;
    let currentProductId = null;

    document.querySelectorAll(".btn-buy").forEach(btn => {
        btn.addEventListener("click", () => {
            const name = btn.dataset.name;
            const price = parseInt(btn.dataset.price);
            const stock = parseInt(btn.dataset.stock);
            const id = btn.dataset.id;

            currentStock = stock;
            currentProductId = id;

            nameEl.textContent = name;
            priceEl.textContent = price.toLocaleString() + "円";
            stockEl.textContent = stock;
            quantityEl.textContent = 1;

            modal.style.display = "flex";
        });
    });

    const btnMinus = document.getElementById("qty-minus");
    const btnPlus = document.getElementById("qty-plus");
    const btnAdd = document.getElementById("purchase-add");
    const btnCancel = document.getElementById("purchase-cancel");

    // 注文個数減
    btnMinus.addEventListener("click", () => {
        let quantity = parseInt(quantityEl.textContent);
        quantityEl.textContent = Math.max(quantity - 1, 1);
    });

    // 注文個数増
    btnPlus.addEventListener("click", () => {
        let quantity = parseInt(quantityEl.textContent);
        // 最大注文個数は99個とする
        quantityEl.textContent = Math.min(quantity + 1, currentStock, 99);
    });

    // カート追加
    btnAdd.addEventListener("click", () => {
        // 後追いで実装
        console.log("商品追加", {
            productId: currentProductId,
            quantity: quantityEl.textContent
        });
        modal.style.display = "none";
    });

    // キャンセル押下
    btnCancel.addEventListener("click", () => {
        modal.style.display = "none";
    });
});