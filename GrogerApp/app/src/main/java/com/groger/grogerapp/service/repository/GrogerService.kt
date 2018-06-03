package com.groger.grogerapp.service.repository

import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.Grocery
import com.groger.grogerapp.service.model.NewCluster
import com.groger.grogerapp.service.model.UserToken
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.adapter.rxjava2.RxJava2CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.*

interface GrogerService {
    @FormUrlEncoded
    @POST("users/token")
    fun login(@Field("grant_type") grant_type: String,
              @Field("username") username : String,
              @Field("password") password: String): Call<UserToken>

    @GET("clusters")
    fun getClusters(@Header("Authorization") token : String) : Call<List<Cluster>>

    @POST("clusters")
    fun addCluster(@Header("Authorization") token : String, @Body cluster: NewCluster) : Call<Cluster>

    @DELETE("clusters/{id}")
    fun removeCluster(@Header("Authorization") token : String, @Path("id") clusterId: Int) : Call<Unit>

    @GET("clusters/{id}/groceries")
    fun getClusterGroceries(@Header("Authorization") token : String, @Path("id") clusterId: Int) : Call<List<Grocery>>


    companion object Factory {
        fun create() : GrogerService {
            val retrofit = Retrofit.Builder()
                    .addCallAdapterFactory(RxJava2CallAdapterFactory.create())
                    .addConverterFactory(GsonConverterFactory.create())
                    .baseUrl("http://192.168.1.29/Groger/api/")
                    .build()

            return retrofit.create(GrogerService::class.java)
        }
    }
}