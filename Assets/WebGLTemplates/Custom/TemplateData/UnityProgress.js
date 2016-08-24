function UnityProgress (dom) {
    this.progress = 0.0;
    this.message = "";
    this.dom = dom;
    var parent = dom.parentNode;
    
    this.SetProgress = function (progress) {
        this.progress = progress;
        this.Update();
    }
    
    this.SetMessage = function (message) {
        this.message = message;
        document.getElementById("loadingInfo").innerHTML = this.message;
    }
    
    this.Clear = function() {
        document.getElementById("loadingBox").style.display = "none";
    }
    
    this.Update = function() {
        var length = 200 * Math.min(this.progress, 1);
        bar = document.getElementById("progressBar");
        bar.style.width = length + "px";
    }
    
    this.Update ();
}