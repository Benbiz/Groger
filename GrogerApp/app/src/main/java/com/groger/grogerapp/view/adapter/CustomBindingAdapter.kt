package com.groger.grogerapp.view.adapter

import android.annotation.SuppressLint
import android.databinding.BindingAdapter
import android.support.design.internal.BottomNavigationItemView
import android.support.design.internal.BottomNavigationMenuView
import android.support.design.widget.BottomNavigationView
import android.util.Log
import android.view.View

@BindingAdapter("visibleGone")
fun setVisibleGone(view: View, show: Boolean){
    if (show)
        view.visibility = View.VISIBLE
    else
        view.visibility = View.GONE
}