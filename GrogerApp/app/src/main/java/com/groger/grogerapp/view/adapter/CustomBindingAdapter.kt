package com.groger.grogerapp.view.adapter

import android.databinding.BindingAdapter
import android.view.View

@BindingAdapter("visibleGone")
fun setVisibleGone(view: View, show: Boolean){
    if (show)
        view.visibility = View.VISIBLE
    else
        view.visibility = View.GONE
}