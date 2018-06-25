package com.groger.grogerapp.view.listener.groceries

import com.groger.grogerapp.service.model.NewGrocery

interface NewGroceryListener {
    fun onNewGrocery(grocery: NewGrocery)
}