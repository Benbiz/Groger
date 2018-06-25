package com.groger.grogerapp.service.model

data class NewGrocery(
        var name : String,
        var description: String,
        var quantity: Int,
        var unit : Int = 0,
        var category : String? = null,
        var picture: String?
)