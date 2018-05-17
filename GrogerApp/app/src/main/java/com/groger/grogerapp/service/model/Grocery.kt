package com.groger.grogerapp.service.model

data class Grocery(
        val id: Int,
        val name: String,
        val description: String,
        val quantity: Int,
        val picture: String?,
        val category: String?
)