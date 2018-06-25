package com.groger.grogerapp.view.listener

import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.NewCluster

interface ClustersListener {
    fun addCluster(cluster : NewCluster)
    fun removeCluster(cluster: Cluster)
    fun clusterSelected(cluster: Cluster)
}