package com.groger.grogerapp.service.model

import java.io.Serializable

data class Grocery(
        val id: Int,
        val name: String,
        val description: String,
        // Not present in grocery, only in clusterGrocery
        val quantity: Int?,
        val picture: String?,
        val category: String?
)  : Serializable