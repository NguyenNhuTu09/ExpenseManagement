package com.example.server.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.server.Service.WalletService;

@RequestMapping("wallets")
@RestController
public class WalletController {
    
    @Autowired
    WalletService walletService;
}
