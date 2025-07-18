mergeInto(LibraryManager.library, {
  IsMobileBrowser: function () {
    if (typeof UnityLoader !== 'undefined' && UnityLoader.SystemInfo) {
      return UnityLoader.SystemInfo.mobile ? 1 : 0;
    }
    // Fallback if UnityLoader not available
    var ua = navigator.userAgent || navigator.vendor || window.opera;
    if (/android/i.test(ua)) return 1;
    if (/iPad|iPhone|iPod/.test(ua) && !window.MSStream) return 1;
    return 0;
  }
});