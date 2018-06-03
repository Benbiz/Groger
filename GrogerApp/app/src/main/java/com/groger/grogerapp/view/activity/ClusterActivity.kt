package com.groger.grogerapp.view.activity

import android.arch.lifecycle.Observer
import android.arch.lifecycle.ViewModelProviders
import android.content.DialogInterface
import android.content.Intent
import android.databinding.DataBindingUtil
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.support.v7.app.AlertDialog
import android.support.v7.widget.LinearLayoutManager
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.ActivityClusterBinding
import com.groger.grogerapp.service.model.Cluster
import com.groger.grogerapp.service.model.NewCluster
import com.groger.grogerapp.view.adapter.ClusterAdapter
import com.groger.grogerapp.view.ui.AddClusterDialog
import com.groger.grogerapp.viewmodel.ClusterViewModel
import com.groger.grogerapp.viewmodel.factory.ClusterViewModelFactory

class ClusterActivity : AppCompatActivity(), ClusterAdapter.OnItemClickListener, AddClusterDialog.NewClusterListener {
    private lateinit var binding: ActivityClusterBinding

    private val clusterAdapter = ClusterAdapter(arrayListOf(), this)

    private var token: String = ""

    override fun onItemLongClick(cluster: Cluster) {
        val alert= AlertDialog.Builder(this)
        alert.apply {
            setMessage("Do you want to delete this cluster ?")
            setTitle("Delete cluster")

            setPositiveButton("Yes", { _: DialogInterface, _: Int ->
                binding.viewModel?.removeCluster(cluster)
            })

            setNegativeButton("No",  { _: DialogInterface, _: Int ->
            })
            setCancelable(false)
            create()
            show()
        }
    }

    override fun onNewCluster(cluster: NewCluster) {
        binding.viewModel?.addCluster(cluster)
    }

    override fun onItemClick(cluster: Cluster) {
        val intent = Intent(this, MainActivity::class.java).apply {
            putExtra("token", token)
            putExtra("cluster", cluster)
        }
        startActivity(intent)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = DataBindingUtil.setContentView(this, R.layout.activity_cluster)

        token = intent.getStringExtra("token")

        val factory = ClusterViewModelFactory(token)
        val vm = ViewModelProviders.of(this, factory)
                .get(ClusterViewModel::class.java)
        binding.viewModel = vm
        binding.executePendingBindings()

        binding.rvClusters.layoutManager = LinearLayoutManager(this)

        binding.rvClusters.adapter = clusterAdapter
        vm.clusters.observe(this, Observer { it?.let {
            clusterAdapter.replaceData(it)
        } })
        vm.loadClusters()

        binding.btnAdd.setOnClickListener {
            val dialog = AddClusterDialog()
            dialog.show(supportFragmentManager, "Add cluster")
        }
    }
}
