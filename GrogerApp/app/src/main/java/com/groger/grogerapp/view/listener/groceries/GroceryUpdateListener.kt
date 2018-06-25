package com.groger.grogerapp.view.listener.groceries

import com.groger.grogerapp.service.model.Grocery

interface GroceryUpdateListener {
    fun onUpdateGrocery(grocery: Grocery)
}