package com.groger.grogerapp.view.ui

import android.app.AlertDialog
import android.app.Dialog
import android.content.Context
import android.databinding.DataBindingUtil
import android.os.Bundle
import android.support.v7.app.AppCompatDialogFragment
import com.groger.grogerapp.R
import com.groger.grogerapp.databinding.AddExistingGroceryBinding
import com.groger.grogerapp.databinding.AddGroceryBinding
import com.groger.grogerapp.service.model.AddGrocery
import com.groger.grogerapp.service.model.NewGrocery
import com.groger.grogerapp.view.listener.groceries.AddGroceryListener
import com.groger.grogerapp.view.listener.groceries.NewGroceryListener

class AddExistingGroceryDialog :  AppCompatDialogFragment() {

    private lateinit var binding: AddExistingGroceryBinding
    private var listener : AddGroceryListener? = null

    val grocery = AddGrocery(1, 0, "")

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        super.onCreateDialog(savedInstanceState)

        val dialog = AlertDialog.Builder(activity)
        val inflater = activity?.layoutInflater

        binding = DataBindingUtil.inflate(inflater!!, R.layout.add_existing_grocery, null, false)


        binding.grocery = grocery
        binding.executePendingBindings()
        dialog.setView(binding.root)
                .setTitle("Create a new grocery")
                .setNegativeButton("Cancel", { _, _ -> })
                .setPositiveButton("Ok", { _, _ ->
                    listener?.onAddGrocery(grocery)
                })
        return dialog.create()
    }

    fun setAddExistingGroceryListener(newListener: AddGroceryListener)
    {
        listener = newListener
    }
}