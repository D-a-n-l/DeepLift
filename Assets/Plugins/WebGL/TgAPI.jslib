mergeInto(LibraryManager.library, {

    GetTelegramUserId: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            console.error("Telegram WebApp API не найден!");
            return 0;
        }

        let tg = window.Telegram.WebApp;
        let user = tg.initDataUnsafe.user;

        if (!user || !user.id) {
            console.warn("ID пользователя недоступен.");
            return 0;
        }

        console.log("ID пользователя:", user.id);
        return user.id;
    },

    SetLockOrientation: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            window.Telegram.WebApp.lockOrientation();
        }
    },

    GetSafeAreaTop: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            return window.Telegram.WebApp.safeAreaInset.top;
        }
    },

    GetSafeAreaRight: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            return window.Telegram.WebApp.safeAreaInset.right;
        }
    },

    GetSafeAreaLeft: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            return window.Telegram.WebApp.safeAreaInset.left;
        }
    },

    GetContentSafeAreaTop: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            return window.Telegram.WebApp.contentSafeAreaInset.top;
        }
    },

    GetContentSafeAreaRight: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            return window.Telegram.WebApp.contentSafeAreaInset.right;
        }
    },

    GetContentSafeAreaLeft: function () {
        if (!window.Telegram || !window.Telegram.WebApp) {
            return window.Telegram.WebApp.contentSafeAreaInset.left;
        }
    },
});