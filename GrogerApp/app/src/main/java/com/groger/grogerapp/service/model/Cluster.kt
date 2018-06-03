package com.groger.grogerapp.service.model

import java.io.Serializable

data class Cluster(
        val id : Int,
        val groceriesQuantity : Int,
        val name: String,
        val description: String
)  : Serializable