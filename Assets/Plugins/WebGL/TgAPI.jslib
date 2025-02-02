mergeInto(LibraryManager.library, {

    GetTelegramUserId: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            console.error("Telegram WebApp API не найден!");
            return 0; // Возвращаем 0, если API недоступен
        }

        let tg = window.Telegram.WebApp;
        let user = tg.initDataUnsafe.user;

        if (!user || !user.id) {
            console.warn("ID пользователя недоступен.");
            return 0; // Возвращаем 0, если ID нет
        }

        console.log("ID пользователя:", user.id);
        return user.id;
    }
});