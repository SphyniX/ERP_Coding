//
//  NSObject+SDKApi.m
//  Unity-iPhone
//
//  Created by xing weizhen on 11/10/14.
//
//

#import "NSObject+SDKApi.h"
#import "UserAvatarController.h"
#import "NSObject+SDKMgr.h"

@implementation SDKApi : NSObject
@synthesize method,param,type,name,charname;

-(void)setMethod:(NSDictionary *)dict{

    method = [dict objectForKey:@"method"];
//    NSString *paramstr = [dict objectForKey:@"param"];
    param = [dict objectForKey:@"param"];
    type = [param objectForKey:@"type"];
    name = [param objectForKey:@"name"];
    charname = [name UTF8String];
//    [method retain];
//    [param retain];
//    [type retain];
//    [name retain];
//    [charname retain];
    
}


-(NSString*)OnGameMesssageRet:(NSString *)paramfromweb
{
    NSDictionary *dict = [SDKMgr JSONStringToDictionary:paramfromweb];
    [self setMethod:dict];
    if (dict != nil) {
        
        if ([method isEqualToString:@"doTakePhoto"]) {
            if([type isEqualToString:@"takephoto"]){
                printf([name UTF8String]);
                [UserAvatarController SettingAvaterFormGameMessageRet:charname AndWithType:false];
            }else{
                printf([name UTF8String]);
                [UserAvatarController SettingAvaterFormGameMessageRet:charname AndWithType:true];
            }
            return @"202";
        }
        
        if([method isEqualToString:@"Update_Ipa"]){
            
            NSURL* url = [[ NSURL alloc ] initWithString :@"http://richer.net.cn/IOSInstall.html"];
            [[UIApplication sharedApplication ] openURL: [url autorelease ]];
            return @"202";
        }
    
    }
    return @"";

    
}
//-(NSString *)OnGameMesssageRet:(NSString *)param
//{
//    return @"1";
//}
-(void)OnGameLaunch
{
    
    
}
static SDKApi *inst;
+(SDKApi *)Instance
{
    if (inst == NULL) {
        inst = [[SDKApi alloc]init];
    }
    return inst;
}
@end
