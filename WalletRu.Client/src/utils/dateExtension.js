Date.prototype.yyyy_mm_dd_hh_mm_ss = function () {
  var yyyy = this.getFullYear().toString()
  var mm = (this.getMonth() + 1).toString()
  var dd = this.getDate().toString()
  var hh = this.getHours().toString()
  var min = this.getMinutes().toString()

  return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]) +
    ' ' + (hh[1] ? hh : "0" + hh[0]) + ':' + (min[1] ? min : "0" + min[0]) + ':00';
}
